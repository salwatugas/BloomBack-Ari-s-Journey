using UnityEngine;

// Script ini bertugas mengatur seluruh interaksi player dengan objek di sekitar
// seperti sampah, debu, dan tanaman (seed)
// Konsep OOP yang digunakan adalah Encapsulation,
// karena seluruh logika interaksi player dikontrol melalui class ini
// Design Pattern yang terlibat adalah Singleton secara tidak langsung,
// melalui penggunaan ToolManager dan manager lainnya
public class PlayerInteract : MonoBehaviour
{
    // Jarak maksimum player bisa berinteraksi
    public float interactRange = 1f;

    // Layer untuk mendeteksi objek sampah dan tanaman
    public LayerMask trashLayer;
    public LayerMask seedLayer;

    // Target interaksi saat ini
    private Trash targetTrash;
    private DustTrash targetDust;
    private SeedPlant activeSeed;

    // Komponen pendukung player
    private Animator animator;
    private SweepSFX sweepSFX;
    private WateringSFX wateringSFX;

    // Ambil semua komponen yang dibutuhkan saat game dimulai
    void Start()
    {
        animator = GetComponent<Animator>();
        sweepSFX = GetComponent<SweepSFX>();
        wateringSFX = GetComponent<WateringSFX>();
    }

    // Update dijalankan setiap frame untuk mengecek input dan interaksi
    void Update()
    {
        targetTrash = null;
        targetDust = null;

        // =========================
        // INTERAKSI TANAMAN (SEED)
        // =========================

        // Deteksi semua tanaman di sekitar player
        Collider2D[] seedHits = Physics2D.OverlapCircleAll(
            transform.position,
            interactRange,
            seedLayer
        );

        // Interaksi tanaman hanya bisa dilakukan jika player memegang alat penyiram
        if (ToolManager.Instance.isHoldingWatering && seedHits.Length > 0)
        {
            // Cari tanaman terdekat dari player
            SeedPlant closestSeed = GetClosestSeed(seedHits);

            // Ganti target jika tanaman yang dituju berubah
            if (activeSeed != closestSeed)
            {
                activeSeed?.ResetProgress();
                activeSeed = closestSeed;
            }

            if (activeSeed != null)
            {
                // Tahan tombol E untuk menyiram tanaman
                if (Input.GetKey(KeyCode.E))
                {
                    activeSeed.WaterProgress(Time.deltaTime);
                    wateringSFX?.PlayWatering();
                }

                // Lepas tombol E untuk menghentikan penyiraman
                if (Input.GetKeyUp(KeyCode.E))
                {
                    activeSeed.ResetProgress();
                    wateringSFX?.ResetWatering();
                    activeSeed = null;
                }

                return;
            }
        }
        else
        {
            // Reset jika player keluar jangkauan atau mengganti alat
            activeSeed?.ResetProgress();
            activeSeed = null;
            wateringSFX?.ResetWatering();
        }

        // =========================
        // DETEKSI SAMPAH
        // =========================

        // Cek apakah ada sampah di sekitar player
        Collider2D trashHit = Physics2D.OverlapCircle(
            transform.position,
            interactRange,
            trashLayer
        );

        if (trashHit == null)
        {
            // Tidak ada sampah, hentikan animasi dan suara
            ResetTrash();
            animator.SetBool("isSweeping", false);
            sweepSFX?.StopSweep();
            return;
        }

        // Ambil jenis sampah yang terdeteksi
        targetTrash = trashHit.GetComponent<Trash>();
        targetDust = trashHit.GetComponent<DustTrash>();

        bool isHoldingBroom = ToolManager.Instance.isHoldingBroom;

        // =========================
        // INTERAKSI DEBU (PAKAI SAPU)
        // =========================
        if (targetDust != null && isHoldingBroom)
        {
            if (Input.GetKey(KeyCode.E))
            {
                bool cleaned = targetDust.CleanProgress(Time.deltaTime);
                animator.SetBool("isSweeping", true);
                sweepSFX?.PlaySweep();

                if (cleaned)
                    targetDust = null;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                ResetTrash();
                animator.SetBool("isSweeping", false);
                sweepSFX?.StopSweep();
            }

            return;
        }

        // =========================
        // INTERAKSI SAMPAH BIASA
        // =========================
        if (targetTrash != null &&
            !isHoldingBroom &&
            ToolManager.Instance.currentTool == ToolManager.ToolType.None)
        {
            if (Input.GetKey(KeyCode.E))
            {
                bool cleaned = targetTrash.CleanProgress(Time.deltaTime);
                if (cleaned)
                    targetTrash = null;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                ResetTrash();
            }

            return;
        }

        ResetTrash();
    }

    // Mencari tanaman terdekat dari player
    SeedPlant GetClosestSeed(Collider2D[] hits)
    {
        SeedPlant closest = null;
        float minDist = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            SeedPlant seed = hit.GetComponent<SeedPlant>();
            if (seed == null) continue;

            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = seed;
            }
        }

        return closest;
    }

    // Mengatur ulang progress pembersihan sampah
    void ResetTrash()
    {
        if (targetTrash != null)
            targetTrash.ResetProgress();
        if (targetDust != null)
            targetDust.ResetProgress();
    }

    // Menampilkan area interaksi player di editor (debug visual)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
