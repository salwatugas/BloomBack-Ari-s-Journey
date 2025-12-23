using UnityEngine;

// MapUIController bertugas mengatur tampilan dan interaksi UI peta
// Script ini memungkinkan player membuka peta dan berpindah area
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang digunakan adalah Singleton dan State (sederhana)
public class MapUIController : MonoBehaviour
{
    // Instance tunggal MapUIController
    public static MapUIController Instance;

    // Panel UI peta
    public GameObject mapPanel;

    // Penanda apakah peta sedang terbuka
    bool isOpen = false;

    void Awake()
    {
        // Pastikan hanya ada satu MapUIController di runtime
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Peta disembunyikan saat awal
        if (mapPanel != null)
            mapPanel.SetActive(false);
    }

    // Membuka peta dan menghentikan waktu game
    public void OpenMap()
    {
        if (isOpen) return;

        mapPanel.SetActive(true);
        isOpen = true;
        Time.timeScale = 0f;
    }

    // Menutup peta dan melanjutkan waktu game
    public void CloseMap()
    {
        if (!isOpen) return;

        mapPanel.SetActive(false);
        isOpen = false;
        Time.timeScale = 1f;
    }

    // Pindah ke area Rumah Ari melalui map
    public void GoToRumahAri()
    {
        // Pastikan waktu game berjalan kembali
        Time.timeScale = 1f;

        // Tutup peta sebelum pindah scene
        CloseMap();

        // Load scene tujuan melalui loading system
        LoadingManager.LoadScene("Area_RumahAri");
    }
}
