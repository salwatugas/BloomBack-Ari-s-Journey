using UnityEngine;
using System.Collections.Generic;

// SeedProgressData digunakan untuk menyimpan data progres pertumbuhan tanaman
// Data ini mencakup tahap pertumbuhan dan sisa waktu tumbuh setiap seed
// Konsep OOP yang digunakan adalah Encapsulation,
// karena seluruh data seed dibungkus dan dikelola dalam satu class khusus
// Design Pattern yang digunakan adalah ScriptableObject,
// sehingga data progres dapat dipisahkan dari logika gameplay
[CreateAssetMenu(menuName = "Game Data/Seed Progress")]
public class SeedProgressData : ScriptableObject
{
    // Struktur data untuk menyimpan progres satu tanaman
    [System.Serializable]
    public class SeedState
    {
        // ID unik tanaman
        public string seedID;

        // Tahap pertumbuhan tanaman
        public SeedPlant.GrowthStage stage;

        // Sisa waktu tumbuh tanaman dalam detik
        public float remainingGrowTime;
    }

    // Daftar seluruh progres tanaman
    public List<SeedState> seeds = new List<SeedState>();

    // Mengambil data progres tanaman berdasarkan ID
    public SeedState GetSeed(string id)
    {
        return seeds.Find(s => s.seedID == id);
    }

    // Menyimpan atau memperbarui progres pertumbuhan tanaman
    public void SaveSeed(string id, SeedPlant.GrowthStage stage, float remainingTime)
    {
        // Cari data seed berdasarkan ID
        SeedState data = GetSeed(id);

        // Jika belum ada, buat data baru
        if (data == null)
        {
            data = new SeedState { seedID = id };
            seeds.Add(data);
        }

        // Simpan tahap pertumbuhan dan sisa waktu
        data.stage = stage;
        data.remainingGrowTime = Mathf.Max(0f, remainingTime);
    }

    // Menghapus seluruh data progres tanaman
    public void ClearAll()
    {
        seeds.Clear();
    }
}
