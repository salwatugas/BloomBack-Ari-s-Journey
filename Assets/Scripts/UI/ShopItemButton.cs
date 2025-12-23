using UnityEngine;

// ShopItemButton bertugas menangani proses pembelian satu item dekorasi
// Script ini dipasang pada tombol di UI shop
// Konsep OOP yang digunakan adalah Encapsulation,
// karena logika pembelian dibungkus dalam satu class
// Design Pattern yang terlibat adalah Command (sederhana)
// serta Singleton secara tidak langsung melalui manager global
public class ShopItemButton : MonoBehaviour
{
    // ID item dekorasi
    public string itemID;

    // Harga item (tetap)
    public int price;

    // Dipanggil saat tombol beli ditekan
    public void OnBuy()
    {
        // Cek apakah inventory dekorasi masih memiliki slot kosong
        if (!DecorationInventory.Instance.HasSpace())
        {
            Debug.Log("Inventory dekorasi penuh");
            return;
        }

        // Cek apakah poin player mencukupi
        if (!GameManager.Instance.SpendPoint(price))
        {
            Debug.Log("Poin tidak cukup");
            return;
        }

        // Tambahkan item ke inventory dekorasi
        bool success = DecorationInventory.Instance.AddDecoration(itemID);

        if (success)
        {
            Debug.Log("Berhasil beli: " + itemID);
        }
    }
}
