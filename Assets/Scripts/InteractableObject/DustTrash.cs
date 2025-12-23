using UnityEngine;

// Script ini bertugas mengatur proses pembersihan debu di lingkungan
// Konsep OOP yang digunakan adalah Encapsulation,
// karena seluruh proses pembersihan, progress, dan reward
// dikontrol langsung oleh class ini
// Design Pattern yang terlibat adalah Singleton secara tidak langsung,
// melalui penggunaan GameManager dan EnergyManager
public class DustTrash : MonoBehaviour
{
    // Waktu yang dibutuhkan untuk membersihkan debu
    public float requiredHoldTime = 2f;

    // Jumlah poin yang didapat setelah debu dibersihkan
    // Nilai ini bisa diatur langsung dari Inspector
    public int pointReward = 15;

    // Waktu hold saat ini
    private float currentHoldTime = 0f;

    // Penanda status pembersihan
    private bool isCleaned = false;
    private bool isInteracting = false;
    private bool isCompleted = false;

    // Referensi progress bar di world space
    public Transform progressBar;
    public Transform fillBar;

    // Skala awal progress bar
    private Vector3 fillOriginalScale;

    // Inisialisasi awal saat object aktif
    void Start()
    {
        // Jika area sudah bersih, debu tidak perlu ditampilkan
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

    // Dipanggil saat player menahan tombol untuk membersihkan debu
    // Mengembalikan true jika debu berhasil dibersihkan
    public bool CleanProgress(float delta)
    {
        // Jika sudah dibersihkan, hentikan proses
        if (isCleaned) return false;

        // Tandai bahwa sedang terjadi interaksi
        isInteracting = true;

        // Debu hanya bisa dibersihkan jika player memegang sapu
        if (!ToolManager.Instance.isHoldingBroom)
            return false;

        // Tambah waktu hold
        currentHoldTime += delta;
        float progress = Mathf.Clamp01(currentHoldTime / requiredHoldTime);

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

            // Tambahkan poin ke pemain
            GameManager.Instance.TrashCleaned(pointReward);

            // Daftarkan pembersihan debu ke sistem energi
            EnergyManager.Instance?.RegisterDustTrashCleaned();

            // Hapus objek debu dari scene
            Destroy(gameObject);
            return true;
        }

        return false;
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
    // untuk mengecek apakah object sedang di-interact
    public bool IsInteracting()
    {
        return isInteracting;
    }

    // Digunakan oleh InteractionDetector
    // untuk mengecek apakah interaksi sudah selesai
    public bool IsInteractionCompleted()
    {
        return isCompleted;
    }
}
