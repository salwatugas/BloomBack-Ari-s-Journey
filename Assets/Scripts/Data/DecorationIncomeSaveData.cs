using UnityEngine;
using System.Collections.Generic;

// DecorationIncomeSaveData digunakan untuk menyimpan data waktu income dekorasi
// Data ini memastikan income dekorasi tetap konsisten walaupun pindah scene
// Konsep OOP yang digunakan adalah Encapsulation,
// karena seluruh data income dibungkus dalam satu class khusus
// Design Pattern yang digunakan adalah ScriptableObject,
// sehingga data income terpisah dari logika gameplay
[CreateAssetMenu(menuName = "Game Data/Decoration Income Save")]
public class DecorationIncomeSaveData : ScriptableObject
{
    // Struktur data untuk menyimpan waktu income satu dekorasi
    [System.Serializable]
    public class IncomeState
    {
        // ID unik dekorasi
        public string decorationID;

        // Waktu income berikutnya (Time.time)
        public float nextIncomeTime;
    }

    // Daftar seluruh data income dekorasi
    public List<IncomeState> incomes = new List<IncomeState>();

    // Mengambil data income berdasarkan ID dekorasi
    public IncomeState Get(string id)
    {
        return incomes.Find(i => i.decorationID == id);
    }

    // Menyimpan atau memperbarui waktu income dekorasi
    public void Save(string id, float nextTime)
    {
        var data = Get(id);
        if (data == null)
        {
            data = new IncomeState { decorationID = id };
            incomes.Add(data);
        }

        data.nextIncomeTime = nextTime;
    }

    // Menghapus data income saat dekorasi dihapus
    public void Remove(string id)
    {
        incomes.RemoveAll(i => i.decorationID == id);
    }

    // Menghapus seluruh data income (digunakan saat mulai game baru)
    public void ClearAll()
    {
        incomes.Clear();
    }
}
