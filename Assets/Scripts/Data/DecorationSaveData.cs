using UnityEngine;
using System.Collections.Generic;

// PlacedDecorationData menyimpan informasi satu dekorasi yang telah ditempatkan
// Data ini berisi ID dekorasi dan posisi penempatannya
[System.Serializable]
public class PlacedDecorationData
{
    // ID dekorasi
    public string itemID;

    // Posisi dekorasi di world
    public Vector2 position;
}

// DecorationSaveData digunakan untuk menyimpan seluruh dekorasi yang telah ditempatkan
// Script ini memastikan dekorasi dapat dimuat ulang saat game dimulai
// Konsep OOP yang digunakan adalah Encapsulation,
// karena seluruh data dekorasi dibungkus dalam satu class
// Design Pattern yang digunakan adalah ScriptableObject
[CreateAssetMenu(menuName = "Game Data/Decoration Save Data")]
public class DecorationSaveData : ScriptableObject
{
    // Daftar dekorasi yang sudah ditempatkan
    public List<PlacedDecorationData> placedDecorations = new List<PlacedDecorationData>();

    // Menambahkan dekorasi baru ke data penyimpanan
    public void Add(string itemID, Vector2 position)
    {
        placedDecorations.Add(new PlacedDecorationData
        {
            itemID = itemID,
            position = position
        });
    }

    // Menghapus dekorasi berdasarkan posisi
    public void RemoveAtPosition(Vector2 position)
    {
        placedDecorations.RemoveAll(d =>
            Vector2.Distance(d.position, position) < 0.1f
        );
    }

    // Menghapus seluruh data dekorasi (digunakan saat new game)
    public void ClearAll()
    {
        placedDecorations.Clear();
    }
}
