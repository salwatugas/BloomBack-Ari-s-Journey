using UnityEngine;
using System.Collections;

// MusicManager bertugas mengelola musik background selama game berjalan
// Script ini menerapkan konsep OOP berupa Encapsulation
// Design Pattern yang digunakan adalah Singleton,
// sehingga hanya ada satu MusicManager yang aktif di seluruh scene
public class MusicManager : MonoBehaviour
{
    // Instance tunggal MusicManager agar bisa diakses dari script lain
    public static MusicManager Instance;

    // AudioSource utama untuk memutar musik
    public AudioSource audioSource;

    // Volume default musik
    public float defaultVolume = 1f;

    // Durasi transisi fade in dan fade out musik
    public float fadeDuration = 0.3f;

    // Daftar musik yang bisa diatur lewat Inspector (opsional)
    public AudioClip[] musicClips;

    // Awake digunakan untuk memastikan hanya ada satu MusicManager
    // dan object ini tidak hilang saat pindah scene
    private void Awake()
    {
        // Cegah duplikasi MusicManager
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Ambil AudioSource jika belum diisi dari Inspector
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        // Musik selalu diulang
        audioSource.loop = true;

        // Ambil pengaturan musik yang tersimpan
        bool musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        audioSource.mute = !musicEnabled;
        audioSource.volume = musicEnabled ? defaultVolume : 0f;

        // Otomatis memutar musik pertama jika tersedia
        if (musicClips != null && musicClips.Length > 0 && !audioSource.isPlaying)
        {
            PlayMusic(musicClips[0]);
        }
    }

    // Memutar musik baru dengan transisi fade
    public void PlayMusic(AudioClip newClip)
    {
        if (newClip == null) return;
        StartCoroutine(FadeToNewMusic(newClip));
    }

    // Memutar musik berdasarkan index dari daftar musik
    public void PlayMusic(int index)
    {
        if (musicClips == null || index < 0 || index >= musicClips.Length) return;
        PlayMusic(musicClips[index]);
    }

    // Mengatur mute atau unmute musik dari menu pengaturan
    public void SetMusicMute(bool mute)
    {
        if (audioSource == null) return;

        audioSource.mute = mute;

        // Jika musik diaktifkan kembali dan belum berjalan, putar ulang
        if (!mute && !audioSource.isPlaying)
            audioSource.Play();
    }

    // Mengatur volume musik
    public void SetMusicVolume(float v)
    {
        if (audioSource == null) return;
        audioSource.volume = v;
    }

    // Coroutine untuk mengganti musik dengan efek fade out dan fade in
    private IEnumerator FadeToNewMusic(AudioClip newClip)
    {
        // Fade out musik lama
        if (audioSource.clip != null)
        {
            float startVol = audioSource.volume;
            float t = 0f;

            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVol, 0f, t / fadeDuration);
                yield return null;
            }
        }

        // Ganti clip musik
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in musik baru
        float t2 = 0f;
        while (t2 < fadeDuration)
        {
            t2 += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, defaultVolume, t2 / fadeDuration);
            yield return null;
        }

        audioSource.volume = defaultVolume;
    }
}
