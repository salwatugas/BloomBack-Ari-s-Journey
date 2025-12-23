using UnityEngine;

// DecorationLoader bertugas memuat ulang dekorasi yang sudah ditempatkan
// Script ini digunakan untuk mengembalikan kondisi dekorasi saat game dimulai
// Konsep OOP yang digunakan adalah Encapsulation,
// karena logika load dan spawn dekorasi dikontrol oleh class ini
// Design Pattern yang terlibat adalah Singleton (melalui DecorationDatabase)
// serta konsep persistence loader untuk memulihkan state game
public class DecorationLoader : MonoBehaviour
{
    // Data penyimpanan dekorasi yang sudah ditempatkan
    public DecorationSaveData saveData;

    // Memuat dekorasi saat scene dimulai
    void Start()
    {
        // Jika tidak ada data dekorasi, hentikan proses
        if (saveData == null || saveData.placedDecorations.Count == 0)
            return;

        // Pastikan database dekorasi tersedia
        if (DecorationDatabase.Instance == null)
        {
            Debug.LogError("DecorationDatabase.Instance NULL");
            return;
        }

        // Ambil seluruh spot dekorasi di scene
        DecorationSpot[] spots = FindObjectsOfType<DecorationSpot>();

        // Loop seluruh data dekorasi yang tersimpan
        foreach (var data in saveData.placedDecorations)
        {
            // Ambil data dekorasi dari database
            var entry = DecorationDatabase.Instance.Get(data.itemID);
            if (entry == null) continue;

            // Spawn prefab dekorasi di posisi yang tersimpan
            GameObject deco = Instantiate(entry.prefab, data.position, Quaternion.identity);

            // Cari spot dekorasi yang sesuai dengan posisi
            foreach (var spot in spots)
            {
                if (Vector2.Distance(spot.transform.position, data.position) < 0.1f)
                {
                    // Tandai spot sebagai terisi
                    spot.SetOccupied();

                    // Hubungkan kembali dekorasi dengan spot-nya
                    // Ini penting untuk sistem remove dekorasi
                    DecorationRemovable removable = deco.GetComponent<DecorationRemovable>();
                    if (removable != null)
                    {
                        removable.sourceSpot = spot;
                        removable.saveData = saveData;
                    }

                    break;
                }
            }
        }
    }
}
