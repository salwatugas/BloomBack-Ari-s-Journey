using UnityEngine;
using UnityEngine.UI;

// SettingsManager bertugas mengatur pengaturan musik dan sound effect
// Script ini menjadi penghubung antara UI Settings dan sistem audio
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang digunakan adalah Facade dan Observer
public class SettingsManager : MonoBehaviour
{
    // Target AudioSource untuk musik dan efek suara
    public AudioSource musicSource;
    public AudioSource audioSource;

    // Toggle UI untuk musik dan audio
    public CustomToggle musicToggle;
    public CustomToggle audioToggle;

    // Key penyimpanan PlayerPrefs
    private const string MUSIC_KEY = "MusicEnabled";
    private const string AUDIO_KEY = "AudioEnabled";

    void Start()
    {
        // Auto bind AudioSource dari manager jika belum di-assign
        if (musicSource == null && MusicManager.Instance != null)
            musicSource = MusicManager.Instance.audioSource;

        if (audioSource == null && AudioManager.Instance != null)
            audioSource = AudioManager.Instance.audioSource;

        // Load status tersimpan
        bool musicEnabled = PlayerPrefs.GetInt(MUSIC_KEY, 1) == 1;
        bool audioEnabled = PlayerPrefs.GetInt(AUDIO_KEY, 1) == 1;

        // Terapkan state ke audio source
        ApplyMusic(musicEnabled);
        ApplyAudio(audioEnabled);

        // Sinkronisasi toggle UI tanpa memicu event ganda
        musicToggle.OnToggleChanged.RemoveAllListeners();
        audioToggle.OnToggleChanged.RemoveAllListeners();

        musicToggle.OnToggleChanged.AddListener(OnMusicToggle);
        audioToggle.OnToggleChanged.AddListener(OnAudioToggle);
    }

    // Dipanggil saat toggle musik berubah
    public void OnMusicToggle(bool enabled)
    {
        PlayerPrefs.SetInt(MUSIC_KEY, enabled ? 1 : 0);
        ApplyMusic(enabled);
    }

    // Dipanggil saat toggle audio berubah
    public void OnAudioToggle(bool enabled)
    {
        PlayerPrefs.SetInt(AUDIO_KEY, enabled ? 1 : 0);
        ApplyAudio(enabled);
    }

    // Menerapkan setting musik ke AudioSource
    void ApplyMusic(bool enabled)
    {
        if (musicSource == null) return;

        musicSource.mute = !enabled;
        musicSource.volume = enabled ? 1f : 0f;

        if (enabled && !musicSource.isPlaying)
            musicSource.Play();
    }

    // Menerapkan setting sound effect ke AudioSource
    void ApplyAudio(bool enabled)
    {
        if (audioSource == null) return;

        audioSource.mute = !enabled;
        audioSource.volume = enabled ? 1f : 0f;
    }
}
