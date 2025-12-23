using UnityEngine;
using UnityEngine.SceneManagement;

// LoadingManager bertugas sebagai pengatur global
// untuk proses perpindahan scene melalui loading scene
// Script ini menyimpan scene tujuan dan memindahkan game
// ke scene loading sebelum masuk ke scene utama
// Konsep OOP yang digunakan adalah Encapsulation (statik)
// Design Pattern yang digunakan adalah Service (helper global)
public static class LoadingManager
{
    // Menyimpan nama scene tujuan yang akan diload
    public static string targetScene;

    // Dipanggil dari mana saja untuk memulai perpindahan scene
    public static void LoadScene(string sceneName)
    {
        // Simpan scene tujuan
        targetScene = sceneName;

        // Pindah ke scene loading
        SceneManager.LoadScene("Loading_Scene");
    }
}
