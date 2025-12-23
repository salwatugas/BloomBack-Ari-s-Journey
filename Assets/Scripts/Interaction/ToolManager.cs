using UnityEngine;
using UnityEngine.SceneManagement;

// ToolManager bertugas mengelola alat yang sedang digunakan oleh player
// seperti sapu dan alat penyiram
// Konsep OOP yang digunakan adalah Encapsulation,
// karena status alat player dikontrol sepenuhnya oleh class ini
// Design Pattern yang digunakan adalah Singleton,
// sehingga hanya ada satu ToolManager yang aktif selama game berjalan
public class ToolManager : MonoBehaviour
{
    // Instance tunggal ToolManager
    public static ToolManager Instance;

    // Jenis alat yang bisa digunakan player
    public enum ToolType
    {
        None,
        Broom,
        Watering
    }

    // Animator player yang akan diubah sesuai alat
    public Animator playerAnimator;

    // Animator override untuk masing-masing alat
    public AnimatorOverrideController broomAOC;
    public AnimatorOverrideController wateringAOC;

    // Animator default player tanpa alat
    private RuntimeAnimatorController defaultController;

    // Alat yang sedang digunakan player
    public ToolType currentTool = ToolType.None;

    // Shortcut untuk cek alat yang sedang dipegang
    public bool isHoldingBroom => currentTool == ToolType.Broom;
    public bool isHoldingWatering => currentTool == ToolType.Watering;

    // Inisialisasi Singleton dan registrasi event perpindahan scene
    void Awake()
    {
        // Cegah duplikasi ToolManager
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Dengarkan event saat scene berpindah
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Lepaskan event listener saat object dihancurkan
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Dipanggil otomatis setiap kali scene baru dimuat
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndBindPlayer();
    }

    // Mencari player di scene dan menghubungkan animator-nya
    void FindAndBindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("[ToolManager] Player not found");
            return;
        }

        playerAnimator = player.GetComponent<Animator>();
        if (playerAnimator == null)
        {
            Debug.LogWarning("[ToolManager] Animator not found on Player");
            return;
        }

        // Simpan animator default player
        defaultController = playerAnimator.runtimeAnimatorController;

        // Terapkan kembali alat yang sedang aktif
        ApplyCurrentTool();
    }

    // Mengatur animator sesuai alat yang sedang digunakan
    void ApplyCurrentTool()
    {
        if (playerAnimator == null) return;

        switch (currentTool)
        {
            case ToolType.Broom:
                playerAnimator.runtimeAnimatorController = broomAOC;
                break;

            case ToolType.Watering:
                playerAnimator.runtimeAnimatorController = wateringAOC;
                break;

            default:
                playerAnimator.runtimeAnimatorController = defaultController;
                break;
        }
    }

    // Mengaktifkan alat sapu (dipanggil dari UI)
    public void EquipBroom()
    {
        // Jika sapu sudah aktif, maka lepas alat
        if (currentTool == ToolType.Broom)
        {
            UnequipTool();
            return;
        }

        currentTool = ToolType.Broom;
        ApplyCurrentTool();
    }

    // Mengaktifkan alat penyiram (dipanggil dari UI)
    public void EquipWatering()
    {
        // Jika alat penyiram sudah aktif, maka lepas alat
        if (currentTool == ToolType.Watering)
        {
            UnequipTool();
            return;
        }

        currentTool = ToolType.Watering;
        ApplyCurrentTool();
    }

    // Melepas semua alat dan kembali ke kondisi default
    public void UnequipTool()
    {
        currentTool = ToolType.None;
        ApplyCurrentTool();
    }
}
