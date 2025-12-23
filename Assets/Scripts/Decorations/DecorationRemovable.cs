using UnityEngine;

// DecorationRemovable bertugas mengatur penghapusan dekorasi dari scene
// Script ini juga memberikan feedback visual saat mode hapus aktif
// Konsep OOP yang digunakan adalah Encapsulation,
// karena logika remove dan visual dikontrol oleh satu class
// Design Pattern yang terlibat adalah Singleton (secara tidak langsung)
// melalui DecorationPlacementManager
public class DecorationRemovable : MonoBehaviour
{
    // Data penyimpanan dekorasi
    public DecorationSaveData saveData;

    // Spot asal dekorasi ini ditempatkan
    public DecorationSpot sourceSpot;

    // Data income dekorasi
    public DecorationIncomeSaveData incomeSaveData;

    // ID dekorasi
    public string decorationID;

    // Warna highlight saat mode hapus aktif
    public Color removeHighlightColor = new Color(1f, 0.4f, 0.4f);

    // Komponen renderer
    SpriteRenderer sr;

    // Warna asli dekorasi
    Color originalColor;

    // Inisialisasi renderer dan warna awal
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update visual berdasarkan mode hapus
    void Update()
    {
        var pm = DecorationPlacementManager.Instance;
        if (pm == null) return;

        // Ubah warna jika mode hapus aktif
        sr.color = pm.IsRemoveMode()
            ? removeHighlightColor
            : originalColor;
    }

    // Menghapus dekorasi melalui manager
    public void RemoveFromManager()
    {
        // Hapus data dekorasi dari save
        if (saveData != null)
            saveData.RemoveAtPosition(transform.position);

        // Hapus data income dekorasi
        if (incomeSaveData != null)
            incomeSaveData.Remove(decorationID);

        // Buka kembali spot dekorasi
        if (sourceSpot != null)
            sourceSpot.ClearOccupied();

        // Hancurkan objek dekorasi
        Destroy(gameObject);
    }
}
