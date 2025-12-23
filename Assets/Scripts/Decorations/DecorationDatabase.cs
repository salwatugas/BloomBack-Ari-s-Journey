using UnityEngine;
using System.Collections.Generic;

// DecorationDatabase bertugas menyimpan dan menyediakan data dekorasi
// seperti icon dan prefab berdasarkan itemID
// Konsep OOP yang digunakan adalah Encapsulation,
// karena data dekorasi hanya diakses melalui method yang disediakan
// Design Pattern yang digunakan adalah Singleton,
// sehingga hanya ada satu database dekorasi selama game berjalan
[System.Serializable]
public class DecorationEntry
{
    // ID unik dekorasi
    public string itemID;

    // Icon dekorasi (untuk UI)
    public Sprite icon;

    // Prefab dekorasi yang akan di-spawn
    public GameObject prefab;
}

public class DecorationDatabase : MonoBehaviour
{
    // Instance tunggal DecorationDatabase
    public static DecorationDatabase Instance;

    // Daftar seluruh dekorasi yang tersedia
    public List<DecorationEntry> decorations;

    // Inisialisasi Singleton
    void Awake()
    {
        Instance = this;
    }

    // Mengambil data dekorasi berdasarkan itemID
    public DecorationEntry Get(string id)
    {
        return decorations.Find(d => d.itemID == id);
    }

    // Mengambil prefab dekorasi berdasarkan itemID
    public GameObject GetPrefab(string id)
    {
        DecorationEntry entry = Get(id);
        return entry != null ? entry.prefab : null;
    }

    // Mengambil icon dekorasi berdasarkan itemID
    // Digunakan jika UI masih membutuhkan icon dari database ini
    public Sprite GetIcon(string id)
    {
        DecorationEntry entry = Get(id);
        return entry != null ? entry.icon : null;
    }
}
