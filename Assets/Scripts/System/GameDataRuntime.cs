using UnityEngine;
using System;


// GameDataRuntime bertugas mengelola seluruh data runtime selama game berjalan
// Data runtime dibuat dengan cara meng-clone template ScriptableObject,
// sehingga data asli (asset) tidak ikut berubah
// Konsep OOP yang digunakan adalah Encapsulation,
// karena seluruh data runtime dikontrol oleh satu class khusus
// Design Pattern yang digunakan adalah Singleton,
// sehingga hanya ada satu pengelola data runtime selama game berjalan
public class GameDataRuntime : MonoBehaviour
{
    public static event Action OnRuntimeDataReset;

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

        CreateRuntimeData();
    }

    // =========================
    // RUNTIME DATA CREATION
    // =========================
    void CreateRuntimeData()
    {
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

    // =========================
    // RESET DATA (NEW GAME)
    // =========================
    // Dipanggil saat player menekan tombol START di Main Menu
    public void ResetAllRuntimeData()
    {
        // Hancurkan data runtime lama
        if (area != null) Destroy(area);
        if (seed != null) Destroy(seed);
        if (decoration != null) Destroy(decoration);
        if (income != null) Destroy(income);

        // Buat ulang data runtime dari template
        CreateRuntimeData();

        // Pastikan runtime data benar-benar fresh & tidak tersimpan
        area.hideFlags = HideFlags.DontSave;
        seed.hideFlags = HideFlags.DontSave;
        decoration.hideFlags = HideFlags.DontSave;
        income.hideFlags = HideFlags.DontSave;

        // Beri tahu semua sistem untuk rebind data runtime
        OnRuntimeDataReset?.Invoke();
    }
    
}
