using UnityEngine;

// Script ini bertugas khusus untuk tombol Exit di Main Menu
// Fungsinya hanya satu: menutup / keluar dari game
// Konsep OOP yang digunakan adalah Single Responsibility,
// karena class ini hanya menangani satu aksi spesifik
// Design pattern yang digunakan bersifat event-driven,
// di mana fungsi dipanggil oleh Button UI
public class ExitGameButton : MonoBehaviour
{
    // Dipanggil saat tombol Exit ditekan
    public void ExitGame()
    {
        // Jika game sudah di-build (exe / apk)
        Application.Quit();

        // Untuk testing di Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
