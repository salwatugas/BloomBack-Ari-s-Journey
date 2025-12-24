using UnityEngine;

// GameManager bertugas sebagai pusat pengatur alur game
// Script ini mengelola sistem poin, progres area, dan pemicu perubahan environment
// Konsep OOP yang digunakan adalah Encapsulation,
// karena data dan aturan game dibungkus dalam satu class
// Design Pattern yang digunakan adalah Singleton,
// serta Facade dan Observer secara konseptual
public class GameManager : MonoBehaviour
{
    // Instance tunggal GameManager
    public static GameManager Instance;

    // Total poin player
    public int playerScore = 0;

    // Data progres area (trash, seed, environment)
    public AreaProgressData areaProgress;

    // Inisialisasi singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // =========================
        // AMBIL DATA RUNTIME
        // =========================
        BindRuntimeData();

        // Dengarkan event reset runtime
        GameDataRuntime.OnRuntimeDataReset += RebindRuntimeData;
    }

    private void OnDestroy()
    {
        GameDataRuntime.OnRuntimeDataReset -= RebindRuntimeData;
    }

    // Hitung total trash saat game dimulai
    private void Start()
    {
        CountTotalTrashIfNeeded();
    }

    // =========================
    // RUNTIME DATA BINDING
    // =========================
    void BindRuntimeData()
    {
        if (GameDataRuntime.Instance == null) return;

        areaProgress = GameDataRuntime.Instance.area;
    }

    // Dipanggil otomatis setelah runtime data di-reset
    void RebindRuntimeData()
    {
        BindRuntimeData();

        // Pastikan data benar-benar kosong
        areaProgress.totalTrash = 0;
        areaProgress.trashCleaned = 0;
        areaProgress.seedGrown = 0;
        areaProgress.isEnvironmentClean = false;

        Debug.Log("[REBIND] AreaProgressData berhasil di-reset & di-bind ulang");
    }

    // Menghitung total trash satu kali di awal area
    void CountTotalTrashIfNeeded()
    {
        if (areaProgress == null) return;

        // Jika sudah pernah dihitung, tidak perlu hitung ulang
        if (areaProgress.totalTrash > 0) return;

        areaProgress.totalTrash =
            FindObjectsOfType<Trash>().Length +
            FindObjectsOfType<DustTrash>().Length;

        Debug.Log($"[INIT] Total Trash: {areaProgress.totalTrash}");
    }

    // Menambahkan poin ke player
    public void AddPoint(int amount)
    {
        playerScore += amount;
        Debug.Log($"+{amount} Point | Total: {playerScore}");
    }

    // Dipanggil saat trash atau dust trash dibersihkan
    // Fungsi ini juga menjadi pemicu perubahan environment
    public void TrashCleaned(int pointReward)
    {
        if (areaProgress == null) return;

        areaProgress.trashCleaned++;
        AddPoint(pointReward);

        Debug.Log($"[TRASH] {areaProgress.trashCleaned} / {areaProgress.totalTrash}");

        CheckEnvironment();
    }

    // Dipanggil saat seed berhasil tumbuh
    // Tidak mempengaruhi kondisi environment
    public void SeedGrown()
    {
        if (areaProgress == null) return;

        areaProgress.seedGrown++;
        Debug.Log($"[SEED] Grown: {areaProgress.seedGrown}");
    }

    // Mengecek apakah environment sudah memenuhi syarat untuk berubah
    void CheckEnvironment()
    {
        if (areaProgress.trashCleaned >= areaProgress.totalTrash &&
            !areaProgress.isEnvironmentClean)
        {
            Debug.Log("[ENV] Semua trash bersih → Pindah ke cerah");

            areaProgress.isEnvironmentClean = true;

            if (EnvironmentManager.Instance != null)
                EnvironmentManager.Instance.TriggerEnvironmentTransition();
            else
                Debug.LogError("[ERROR] EnvironmentManager.Instance NULL");
        }
    }

    // Mengurangi poin player (digunakan saat belanja)
    public bool SpendPoint(int amount)
    {
        if (playerScore < amount)
            return false;

        playerScore -= amount;
        return true;
    }

    // Mengambil total poin player
    public int GetScore()
    {
        return playerScore;
    }

    // =========================
    // RESET GAME DATA (START GAME)
    // =========================
    public void ResetGame()
    {
        // Reset poin player
        playerScore = 0;

        if (areaProgress == null) return;

        // Reset progres area
        areaProgress.trashCleaned = 0;
        areaProgress.seedGrown = 0;
        areaProgress.isEnvironmentClean = false;

        // Reset total trash agar dihitung ulang saat gameplay dimulai
        areaProgress.totalTrash = 0;

        Debug.Log("[RESET] GameManager & AreaProgress reset");
    }
}
