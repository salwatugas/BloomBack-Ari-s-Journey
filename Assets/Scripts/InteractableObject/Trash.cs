using UnityEngine;

// Script ini bertugas mengatur proses pembersihan sampah biasa
// Konsep OOP yang digunakan adalah Encapsulation,
// karena seluruh status pembersihan, progress, dan reward
// dikelola langsung oleh class ini
// Design Pattern yang terlibat adalah Singleton secara tidak langsung,
// melalui penggunaan GameManager dan EnergyManager
public class Trash : MonoBehaviour
{
    // Waktu yang dibutuhkan untuk membersihkan sampah
    public float requiredHoldTime = 2f;

    // Jumlah poin yang didapat setelah sampah dibersihkan
    // Nilai ini bisa diatur langsung dari Inspector
    public int pointReward = 10;

    // Waktu hold saat ini
    private float currentHoldTime = 0f;

    // Penanda status pembersihan sampah
    private bool isCleaned = false;
    private bool isInteracting = false;
    private bool isCompleted = false;

    // Referensi progress bar yang tampil di world space
    public Transform progressBar;
    public Transform fillBar;

    // Skala awal progress bar
    private Vector3 fillOriginalScale;

    // Inisialisasi awal saat object aktif
    void Start()
    {
        // Jika area sudah bersih, sampah tidak perlu ditampilkan
        if (GameManager.Instance != null &&
            GameManager.Instance.areaProgress.isEnvironmentClean)
        {
            Destroy(gameObject);
            return;
        }

        // Simpan skala awal progress bar
        if (fillBar != null)
            fillOriginalScale = fillBar.localScale;

        // Sembunyikan progress bar saat awal
        if (progressBar != null)
            progressBar.gameObject.SetActive(false);
    }

    // Dipanggil saat player menahan tombol untuk membersihkan sampah
    // Mengembalikan true jika sampah berhasil dibersihkan
    public bool CleanProgress(float delta)
    {
        // Jika sudah dibersihkan, hentikan proses
        if (isCleaned) return false;

        // Tandai bahwa sedang terjadi interaksi
        isInteracting = true;

        // Jika tidak bisa dibersihkan, tampilkan warning yang sesuai
        if (!CanBeCleaned())
        {
            ShowWarningIfNeeded();
            return false;
        }

        // Tambah waktu hold
        currentHoldTime += delta;
        float progress = Mathf.Clamp01(
            currentHoldTime / requiredHoldTime
        );

        // Tampilkan progress bar
        if (progressBar != null)
            progressBar.gameObject.SetActive(true);

        // Update isi progress bar
        if (fillBar != null)
            fillBar.localScale = new Vector3(
                progress * fillOriginalScale.x,
                fillOriginalScale.y,
                fillOriginalScale.z
            );

        // Jika waktu hold sudah cukup
        if (currentHoldTime >= requiredHoldTime)
        {
            isCleaned = true;
            isCompleted = true;
            isInteracting = false;

            // Hapus progress bar
            if (progressBar != null)
                Destroy(progressBar.gameObject);

            // Putar sound effect pengambilan sampah
            GetComponent<TrashSFX>()?.PlayPickup();

            // Tambahkan poin ke pemain
            GameManager.Instance.TrashCleaned(pointReward);

            // Daftarkan pembersihan sampah ke sistem energi
            EnergyManager.Instance?.RegisterTrashCleaned();

            // Hapus objek sampah dari scene
            Destroy(gameObject);
            return true;
        }

        return false;
    }

    // =========================
    // VALIDATION (TRASH)
    // =========================
    bool CanBeCleaned()
    {
        // Harus TIDAK memegang alat apapun
        if (ToolManager.Instance.currentTool != ToolManager.ToolType.None)
            return false;

        // Harus punya energi
        if (!EnergyManager.Instance.HasEnergy())
            return false;

        return true;
    }

    // =========================
    // WARNING HANDLER (TRASH)
    // =========================
    void ShowWarningIfNeeded()
    {
        // Jika sedang memegang sapu
        if (ToolManager.Instance.currentTool ==
            ToolManager.ToolType.Broom)
        {
            ToolManager.Instance.ShowWrongToolWarning(
                ToolManager.ToolType.Broom
            );
            return;
        }

        // Jika sedang memegang penyiram
        if (ToolManager.Instance.currentTool ==
            ToolManager.ToolType.Watering)
        {
            ToolManager.Instance.ShowWrongToolWarning(
                ToolManager.ToolType.Watering
            );
            return;
        }

        // Jika energi habis
        if (!EnergyManager.Instance.HasEnergy())
        {
            EnergyManager.Instance.ShowEnergyWarning();
            return;
        }
    }

    // Mengatur ulang progress pembersihan saat interaksi dibatalkan
    public void ResetProgress()
    {
        currentHoldTime = 0f;
        isInteracting = false;

        // Sembunyikan progress bar
        if (progressBar != null)
            progressBar.gameObject.SetActive(false);

        // Reset isi progress bar
        if (fillBar != null)
            fillBar.localScale =
                new Vector3(0, fillOriginalScale.y, fillOriginalScale.z);
    }

    // Digunakan oleh InteractionDetector
    // untuk mengecek apakah sampah sedang di-interact
    public bool IsInteracting()
    {
        return isInteracting;
    }

    // Digunakan oleh InteractionDetector
    // untuk menentukan apakah interaksi sudah selesai
    public bool IsInteractionCompleted()
    {
        return isCompleted;
    }
}
