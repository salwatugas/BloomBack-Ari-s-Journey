using UnityEngine;

// Script ini bertugas memicu pemutaran musik khusus untuk gameplay
// Konsep OOP yang digunakan adalah Encapsulation,
// karena script ini tidak mengelola musik secara langsung,
// tetapi menggunakan MusicManager sebagai pengelola utama
// Design Pattern yang terlibat adalah Singleton melalui MusicManager
public class GameplayMusic : MonoBehaviour
{
    // Musik yang akan diputar saat gameplay dimulai
    public AudioClip gameplayClip;

    // Start dipanggil saat scene gameplay pertama kali aktif
    void Start()
    {
        // Cek keamanan jika MusicManager atau musik belum tersedia
        if (MusicManager.Instance == null || gameplayClip == null)
            return;

        // Jangan memutar ulang musik jika musik yang sama sudah sedang diputar
        if (MusicManager.Instance.audioSource.clip == gameplayClip &&
            MusicManager.Instance.audioSource.isPlaying)
        {
            return;
        }

        // Meminta MusicManager untuk memutar musik gameplay
        MusicManager.Instance.PlayMusic(gameplayClip);
    }
}
