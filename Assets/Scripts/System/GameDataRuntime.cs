using UnityEngine;

// GameDataRuntime bertugas mengelola seluruh data runtime selama game berjalan
// Data runtime dibuat dengan cara meng-clone template ScriptableObject,
// sehingga data asli (asset) tidak ikut berubah
// Konsep OOP yang digunakan adalah Encapsulation,
// karena seluruh data runtime dikontrol oleh satu class khusus
// Design Pattern yang digunakan adalah Singleton,
// sehingga hanya ada satu pengelola data runtime selama game berjalan
public class GameDataRuntime : MonoBehaviour
{
    // Instance tunggal GameDataRuntime
    public static GameDataRuntime Instance;

    // Template data yang digunakan sebagai dasar clone
    public AreaProgressData areaTemplate;
    public SeedProgressData seedTemplate;
    public DecorationSaveData decorationTemplate;
    public DecorationIncomeSaveData incomeTemplate;

    // Data runtime yang digunakan selama permainan
    public AreaProgressData area;
    public SeedProgressData seed;
    public DecorationSaveData decoration;
    public DecorationIncomeSaveData income;

    // Inisialisasi Singleton dan clone data runtime saat game dimulai
    void Awake()
    {
        // Cegah duplikasi GameDataRuntime
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Membuat salinan data runtime dari template
        area = Instantiate(areaTemplate);
        seed = Instantiate(seedTemplate);
        decoration = Instantiate(decorationTemplate);
        income = Instantiate(incomeTemplate);
    }

    // Mengatur ulang seluruh data runtime ke kondisi awal
    public void ResetAllRuntimeData()
    {
        // Hapus data runtime lama
        Destroy(area);
        Destroy(seed);
        Destroy(decoration);
        Destroy(income);

        // Buat ulang data runtime dari template
        area = Instantiate(areaTemplate);
        seed = Instantiate(seedTemplate);
        decoration = Instantiate(decorationTemplate);
        income = Instantiate(incomeTemplate);

        // Pastikan data runtime tidak tersimpan sebagai asset
        area.hideFlags = HideFlags.DontSave;
        seed.hideFlags = HideFlags.DontSave;
        decoration.hideFlags = HideFlags.DontSave;
        income.hideFlags = HideFlags.DontSave;
    }
}
