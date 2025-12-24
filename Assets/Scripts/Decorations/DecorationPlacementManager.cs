using UnityEngine;

// DecorationPlacementManager bertugas mengatur pemilihan, pemasangan,
// dan penghapusan dekorasi di dalam game
// Script ini menjadi pusat kontrol antara UI dekorasi dan gameplay
// Konsep OOP yang digunakan adalah Encapsulation,
// karena status dekorasi dan mode dikontrol oleh satu class
// Design Pattern yang digunakan adalah Singleton dan State (sederhana)
public class DecorationPlacementManager : MonoBehaviour
{
    // Instance tunggal manager placement dekorasi
    public static DecorationPlacementManager Instance;

    // Data dekorasi yang sedang dipilih
    public string selectedItemID;
    public GameObject selectedPrefab;

    // Penanda apakah sedang dalam mode hapus dekorasi
    public bool isRemoveMode = false;

    // Inisialisasi Singleton
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Mengecek input global setiap frame
    void Update()
    {
        // Tombol ESC digunakan untuk keluar dari mode apapun
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelPlacement();
            isRemoveMode = false;
        }

        // Jika sedang dalam mode hapus, klik kiri mouse akan mencoba menghapus dekorasi
        if (isRemoveMode && Input.GetMouseButtonDown(0))
        {
            TryRemoveAtMouse();
        }
    }

    // Mencoba menghapus dekorasi di posisi mouse
    void TryRemoveAtMouse()
    {
        Vector2 mouseWorldPos =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);

        if (hit == null) return;

        // Cek apakah objek memiliki komponen DecorationRemovable
        DecorationRemovable removable =
            hit.GetComponent<DecorationRemovable>();

        if (removable != null)
        {
            removable.RemoveFromManager();
        }
    }

    // Mengaktifkan atau menonaktifkan mode hapus dekorasi
    public void ToggleRemoveMode()
    {
        isRemoveMode = !isRemoveMode;
        if (isRemoveMode)
            CancelPlacement();
    }

    // Mengecek apakah sedang dalam mode hapus
    public bool IsRemoveMode() => isRemoveMode;

    // Memilih dekorasi dari UI inventory
    public void SelectDecoration(string itemID, GameObject prefab)
    {
        // Jika dekorasi yang sama dipilih ulang, batalkan selection
        if (selectedItemID == itemID && HasSelection())
        {
            CancelPlacement();
            return;
        }

        selectedItemID = itemID;
        selectedPrefab = prefab;
        isRemoveMode = false;
    }

    // Membatalkan proses placement dekorasi
    public void CancelPlacement()
    {
        selectedItemID = null;
        selectedPrefab = null;
    }

    // Mengecek apakah ada dekorasi yang sedang dipilih
    public bool HasSelection()
    {
        return selectedPrefab != null;
    }

    // =========================
    // RESET PLACEMENT STATE (START GAME)
    // =========================
    public void ResetPlacementState()
    {
        // Batalkan dekorasi yang sedang dipilih
        CancelPlacement();

        // Matikan mode hapus
        isRemoveMode = false;

        Debug.Log("[RESET] DecorationPlacementManager reset");
    }
}
