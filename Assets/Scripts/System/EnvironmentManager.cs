using UnityEngine;
using System.Threading.Tasks;

// EnvironmentManager bertugas mengatur kondisi visual lingkungan
// Script ini menentukan apakah environment tampil gelap atau cerah
// berdasarkan progres area yang tersimpan di runtime data
// Konsep OOP yang digunakan adalah Encapsulation,
// karena seluruh logika environment dibungkus dalam satu class
// Design Pattern yang digunakan adalah Singleton dan State (sederhana)
public class EnvironmentManager : MonoBehaviour
{
    // Instance tunggal EnvironmentManager
    public static EnvironmentManager Instance;

    // GameObject environment versi kotor / gelap
    public GameObject darkEnvironment;

    // GameObject environment versi bersih / cerah
    public GameObject brightEnvironment;

    // Lama layar hitam saat transisi
    public float blackScreenHoldTime = 2f;

    // Penanda agar transisi tidak dipicu berulang
    private bool isTransitioning = false;

    // Data progres area saat runtime
    private AreaProgressData areaProgress;

    // Inisialisasi singleton
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Ambil runtime data dan terapkan kondisi environment
    private void Start()
    {
        areaProgress = GameDataRuntime.Instance.area;
        ApplyEnvironmentState();
    }

    // Menerapkan visual environment berdasarkan state global
    void ApplyEnvironmentState()
    {
        bool isClean = areaProgress != null && areaProgress.isEnvironmentClean;

        darkEnvironment.SetActive(!isClean);
        brightEnvironment.SetActive(isClean);
    }

    // Dipanggil oleh sistem lain (misalnya GameManager)
    // untuk memulai transisi environment
    public void TriggerEnvironmentTransition()
    {
        if (isTransitioning) return;

        isTransitioning = true;
        TransitionRoutine();
    }

    // Proses transisi environment secara async
    private async void TransitionRoutine()
    {
        // Fade layar ke hitam
        if (ScreenFader.Instance != null)
            await ScreenFader.Instance.FadeOut();

        // Tahan layar hitam beberapa saat
        await Task.Delay((int)(blackScreenHoldTime * 1000));

        // Ubah state global environment menjadi bersih
        if (areaProgress != null)
            areaProgress.isEnvironmentClean = true;

        // Terapkan visual berdasarkan state terbaru
        ApplyEnvironmentState();

        // Fade layar kembali terang
        if (ScreenFader.Instance != null)
            await ScreenFader.Instance.FadeIn();

        // Buka kembali akses transisi
        isTransitioning = false;
    }
}
