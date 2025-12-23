using UnityEngine;

// AudioManager bertugas mengelola pemutaran Sound Effect (SFX) secara global
// Script ini menerapkan konsep OOP berupa Encapsulation
// Design Pattern yang digunakan adalah Singleton,
// sehingga hanya ada satu AudioManager selama game berjalan
public class AudioManager : MonoBehaviour
{
    // Instance tunggal AudioManager agar bisa diakses dari script lain
    public static AudioManager Instance;

    // AudioSource utama yang digunakan untuk memutar semua SFX
    public AudioSource audioSource;

    // Awake digunakan untuk memastikan hanya ada satu AudioManager
    // dan object ini tidak hancur saat pindah scene
    private void Awake()
    {
        // Jika sudah ada instance lain, object ini dihancurkan
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Set instance dan buat AudioManager tetap hidup antar scene
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Jika AudioSource belum diisi di Inspector, ambil dari GameObject
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    // Fungsi untuk memutar sound effect satu kali
    public void PlaySFX(AudioClip clip)
    {
        // Cek keamanan agar tidak terjadi error
        if (clip == null || audioSource == null) return;

        audioSource.PlayOneShot(clip);
    }

    // Fungsi untuk mengatur volume sound effect
    public void SetSFXVolume(float v)
    {
        if (audioSource != null)
            audioSource.volume = v;
    }

    // Fungsi untuk mengaktifkan atau menonaktifkan sound effect
    public void SetSFXMute(bool mute)
    {
        if (audioSource != null)
            audioSource.mute = mute;
    }
}
