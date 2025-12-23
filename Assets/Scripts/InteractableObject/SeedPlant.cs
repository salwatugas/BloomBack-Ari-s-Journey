using UnityEngine;
using System.Collections;

// Script ini bertugas mengatur perilaku tanaman yang bisa disiram
// mulai dari tahap seed hingga tumbuh penuh
// Konsep OOP yang digunakan adalah Encapsulation,
// karena seluruh logika pertumbuhan, progress, dan reward
// dikontrol oleh class ini
// Design Pattern yang terlibat adalah State Pattern,
// melalui penggunaan enum GrowthStage
// serta Singleton secara tidak langsung,
// melalui GameManager, EnergyManager, dan GameDataRuntime
public class SeedPlant : MonoBehaviour
{
    // Tahapan pertumbuhan tanaman
    public enum GrowthStage { Seed, Medium, Tall }

    [Header("Identity")]
    // ID unik tanaman untuk kebutuhan save runtime
    public string seedID;

    // Data progres seed selama game berjalan
    private SeedProgressData seedProgress;

    [Header("Growth State")]
    // Tahap pertumbuhan saat ini
    public GrowthStage currentStage = GrowthStage.Seed;

    // Lama menahan tombol untuk menyiram
    public float wateringHoldTime = 2f;

    // Waktu tumbuh setelah penyiraman selesai
    public float growDelay = 5f;

    private float currentHoldTime = 0f;
    private bool isGrowing = false;
    private bool isInteracting = false;
    private Coroutine growCoroutine;

    [Header("Reward")]
    // Poin yang didapat setiap kali tanaman berhasil disiram
    public int pointPerGrow = 15;

    [Header("Sprites")]
    // Sprite untuk setiap tahap pertumbuhan
    public Sprite seedSprite;
    public Sprite mediumSprite;
    public Sprite tallSprite;

    private SpriteRenderer sr;

    [Header("Progress Bar")]
    // UI progress bar saat proses menyiram
    public Transform progressBar;
    public Transform fillBar;

    private Vector3 fillOriginalScale;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        seedProgress = GameDataRuntime.Instance.seed;

        // Load progres seed jika sudah pernah disimpan
        var saved = seedProgress.GetSeed(seedID);
        if (saved != null)
        {
            currentStage = saved.stage;

            // Jika masih dalam proses tumbuh, lanjutkan coroutine
            if (saved.remainingGrowTime > 0f && currentStage != GrowthStage.Tall)
            {
                isGrowing = true;
                growCoroutine = StartCoroutine(
                    GrowPlant(saved.remainingGrowTime)
                );
            }
        }

        ApplyVisualByStage();

        if (fillBar != null)
            fillOriginalScale = fillBar.localScale;

        if (progressBar != null)
            progressBar.gameObject.SetActive(false);
    }

    // Dipanggil saat player menahan tombol untuk menyiram tanaman
    public bool WaterProgress(float delta)
    {
        if (!CanBeWatered())
            return false;

        isInteracting = true;

        currentHoldTime += delta;
        float progress = Mathf.Clamp01(
            currentHoldTime / wateringHoldTime
        );

        if (progressBar != null)
            progressBar.gameObject.SetActive(true);

        if (fillBar != null)
            fillBar.localScale = new Vector3(
                progress * fillOriginalScale.x,
                fillOriginalScale.y,
                fillOriginalScale.z
            );

        // Jika waktu hold sudah cukup
        if (currentHoldTime >= wateringHoldTime)
        {
            CompleteWatering();
            return true;
        }

        return false;
    }

    // Mengecek apakah tanaman masih bisa disiram
    bool CanBeWatered()
    {
        if (!ToolManager.Instance.isHoldingWatering)
            return false;

        if (!EnergyManager.Instance.HasEnergy())
            return false;

        if (isGrowing)
            return false;

        if (currentStage == GrowthStage.Tall)
            return false;

        return true;
    }

    // Proses setelah penyiraman selesai
    void CompleteWatering()
    {
        if (!EnergyManager.Instance.UseEnergy(1))
            return;

        isInteracting = false;
        ResetProgress();

        // Tambahkan poin ke pemain
        GameManager.Instance?.AddPoint(pointPerGrow);

        isGrowing = true;

        // Simpan progres ke runtime data
        seedProgress.SaveSeed(seedID, currentStage, growDelay);
        growCoroutine = StartCoroutine(GrowPlant(growDelay));
    }

    // Coroutine proses tumbuh tanaman
    IEnumerator GrowPlant(float remainingTime)
    {
        float timeLeft = remainingTime;

        while (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            seedProgress.SaveSeed(seedID, currentStage, timeLeft);
            yield return null;
        }

        AdvanceStage();
        isGrowing = false;
        growCoroutine = null;
    }

    // Naik ke tahap pertumbuhan berikutnya
    void AdvanceStage()
    {
        if (currentStage == GrowthStage.Seed)
            currentStage = GrowthStage.Medium;
        else if (currentStage == GrowthStage.Medium)
            currentStage = GrowthStage.Tall;

        seedProgress.SaveSeed(seedID, currentStage, 0f);
        ApplyVisualByStage();

        // Jika sudah maksimal, laporkan ke GameManager
        if (currentStage == GrowthStage.Tall)
            GameManager.Instance?.SeedGrown();
    }

    // Reset progress bar saat interaksi dibatalkan
    public void ResetProgress()
    {
        currentHoldTime = 0f;
        isInteracting = false;

        if (progressBar != null)
            progressBar.gameObject.SetActive(false);

        if (fillBar != null)
            fillBar.localScale =
                new Vector3(0, fillOriginalScale.y, fillOriginalScale.z);
    }

    // Mengubah sprite sesuai tahap pertumbuhan
    void ApplyVisualByStage()
    {
        if (sr == null) return;

        sr.sprite = currentStage switch
        {
            GrowthStage.Seed => seedSprite,
            GrowthStage.Medium => mediumSprite,
            GrowthStage.Tall => tallSprite,
            _ => sr.sprite
        };
    }

    // Digunakan oleh InteractionDetector
    // untuk mengecek apakah tanaman sedang di-interact
    public bool IsInteracting()
    {
        return isInteracting;
    }

    // Digunakan oleh InteractionDetector
    // untuk menentukan apakah interaksi sudah benar-benar selesai
    // Interaksi seed dianggap selesai jika tanaman sudah tumbuh penuh
    public bool IsInteractionCompleted()
    {
        return currentStage == GrowthStage.Tall;
    }
}
