using UnityEngine;

// DecorationSpot bertugas sebagai titik penempatan dekorasi
// Script ini mengatur apakah spot bisa ditempati dan menangani proses penempatan dekorasi
// Konsep OOP yang digunakan adalah Encapsulation,
// karena status spot dan logika penempatan dikontrol oleh class ini
// Design Pattern yang terlibat adalah Singleton secara tidak langsung
// melalui penggunaan manager global
public class DecorationSpot : MonoBehaviour
{
    // Data penyimpanan dekorasi
    public DecorationSaveData saveData;

    // Penanda apakah spot sudah terisi dekorasi
    bool isOccupied = false;

    // Renderer untuk indikator visual spot
    SpriteRenderer sr;

    // Inisialisasi renderer
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    // Update indikator visual berdasarkan mode placement
    void Update()
    {
        // Jika spot sudah terisi, indikator tidak ditampilkan
        if (isOccupied) 
        {
            sr.enabled = false;
            return;
        }

        var pm = DecorationPlacementManager.Instance;
        if (pm == null) return;

        // Indikator hanya muncul saat mode placement aktif (bukan remove)
        sr.enabled = pm.HasSelection();
    }

    // Dipanggil saat spot diklik
    void OnMouseDown()
    {
        // Jika spot sudah terisi, hentikan proses
        if (isOccupied) return;

        var pm = DecorationPlacementManager.Instance;
        if (pm == null || !pm.HasSelection()) return;

        // Gunakan energi untuk menempatkan dekorasi
        if (!EnergyManager.Instance.UseEnergy(1)) return;

        string itemID = pm.selectedItemID;
        GameObject prefab = pm.selectedPrefab;

        // Spawn dekorasi di posisi spot
        GameObject deco =
            Instantiate(prefab, transform.position, Quaternion.identity);

        // Hubungkan dekorasi dengan spot dan data save
        DecorationRemovable removable =
            deco.GetComponent<DecorationRemovable>();

        if (removable != null)
        {
            removable.sourceSpot = this;
            removable.saveData = saveData;
        }

        // Simpan data dekorasi
        saveData.Add(itemID, transform.position);

        // Hapus dekorasi dari inventory player
        DecorationInventory.Instance.ownedDecorations.Remove(itemID);
        DecorationInventory.Instance.onInventoryChanged?.Invoke();

        // Tandai spot sebagai terisi
        SetOccupied();

        // Keluar dari mode placement
        pm.CancelPlacement();
    }

    // Menandai spot sebagai terisi
    public void SetOccupied()
    {
        isOccupied = true;
        sr.enabled = false;
    }

    // Mengosongkan kembali spot
    public void ClearOccupied()
    {
        isOccupied = false;
    }
}
