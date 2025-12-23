using UnityEngine;

// Script ini bertugas memutar sound effect saat player melakukan aksi menyiram tanaman
// Konsep OOP yang digunakan adalah Encapsulation,
// karena script ini hanya mengelola satu jenis suara
// Design Pattern yang terlibat adalah Singleton melalui AudioManager
public class WateringSFX : MonoBehaviour
{
    // Sound effect untuk aksi menyiram
    public AudioClip wateringSFX;

    // Penanda agar suara tidak diputar berulang dalam satu aksi menyiram
    private bool hasPlayed = false;

    // Memutar sound effect penyiraman
    public void PlayWatering()
    {
        // Cegah suara diputar berulang
        if (hasPlayed) return;

        // Cek keamanan sebelum memutar suara
        if (AudioManager.Instance != null && wateringSFX != null)
        {
            AudioManager.Instance.PlaySFX(wateringSFX);
            hasPlayed = true;
        }
    }

    // Mengatur ulang status agar suara bisa diputar kembali
    // Biasanya dipanggil saat aksi menyiram selesai
    public void ResetWatering()
    {
        hasPlayed = false;
    }
}
