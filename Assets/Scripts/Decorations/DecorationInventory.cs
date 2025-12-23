using UnityEngine;
using System.Collections.Generic;

// DecorationInventory bertugas mengelola inventory dekorasi player
// Script ini menyimpan daftar dekorasi yang dimiliki dan membatasi jumlah slot
// Konsep OOP yang digunakan adalah Encapsulation,
// karena data inventory dikontrol oleh satu class khusus
// Design Pattern yang digunakan adalah Singleton dan Observer (sederhana),
// Singleton untuk satu inventory global,
// Observer melalui event agar UI dapat merespons perubahan inventory
public class DecorationInventory : MonoBehaviour
{
    // Instance tunggal DecorationInventory
    public static DecorationInventory Instance;

    // Jumlah slot maksimum dekorasi
    public int maxSlots = 3;

    // Daftar ID dekorasi yang dimiliki player
    public List<string> ownedDecorations = new List<string>();

    // Event yang dipanggil saat inventory berubah
    public System.Action onInventoryChanged;

    // Inisialisasi Singleton
    void Awake()
    {
        // Cegah duplikasi inventory
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Mengecek apakah inventory masih memiliki slot kosong
    public bool HasSpace()
    {
        return ownedDecorations.Count < maxSlots;
    }

    // Menambahkan dekorasi ke inventory
    // Mengembalikan true jika berhasil ditambahkan
    public bool AddDecoration(string itemID)
    {
        // Cek apakah slot masih tersedia
        if (!HasSpace())
            return false;

        // Tambahkan dekorasi ke inventory
        ownedDecorations.Add(itemID);

        // Beri tahu UI bahwa inventory berubah
        onInventoryChanged?.Invoke();
        return true;
    }
}
