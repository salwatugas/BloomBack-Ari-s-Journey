using UnityEngine;

// PakDanuNPC bertugas mengatur interaksi player dengan NPC Pak Danu
// Script ini hanya berfungsi sebagai pemicu untuk membuka UI shop
// Konsep OOP yang digunakan adalah Encapsulation,
// karena logika interaksi NPC dibungkus dalam satu class
// Design Pattern yang digunakan adalah Command (sederhana)
// serta Singleton secara tidak langsung melalui UI shop
public class PakDanuNPC : MonoBehaviour
{
    // Penanda apakah player berada di area NPC
    private bool playerInRange = false;

    // Mengecek input interaksi setiap frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Membuka UI shop Pak Danu
            PakDanuShopUI.Instance?.OpenShop();
        }
    }

    // Mendeteksi player masuk ke area NPC
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    // Mendeteksi player keluar dari area NPC
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
