using UnityEngine;

// Script ini bertugas memicu musik khusus untuk scene Main Menu
// Konsep OOP yang digunakan adalah Encapsulation,
// karena script ini hanya memanggil MusicManager tanpa mengelola audio secara langsung
// Design Pattern yang terlibat adalah Singleton melalui MusicManager
public class MainMenuMusic : MonoBehaviour
{
    // Musik yang akan diputar saat Main Menu aktif
    public AudioClip mainMenuClip;

    // Start dipanggil saat scene Main Menu pertama kali dimuat
    void Start()
    {
        // Cek keamanan sebelum memutar musik
        if (MusicManager.Instance != null && mainMenuClip != null)
        {
            MusicManager.Instance.PlayMusic(mainMenuClip);
        }
    }
}
