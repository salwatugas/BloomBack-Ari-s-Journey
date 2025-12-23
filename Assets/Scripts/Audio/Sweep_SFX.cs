using UnityEngine;

// Script ini bertugas memutar sound effect saat player melakukan aksi menyapu
// Konsep OOP yang digunakan adalah Encapsulation,
// karena script ini hanya bertanggung jawab pada satu jenis suara
// Design Pattern yang terlibat adalah Singleton melalui AudioManager
public class SweepSFX : MonoBehaviour
{
    // Sound effect untuk aksi menyapu
    public AudioClip sweepSFX;

    // Penanda agar suara tidak diputar berulang-ulang
    private bool isPlaying = false;

    // Memutar sound effect menyapu
    public void PlaySweep()
    {
        // Cegah suara diputar berulang saat aksi masih berlangsung
        if (isPlaying) return;

        // Cek keamanan sebelum memutar suara
        if (AudioManager.Instance != null && sweepSFX != null)
        {
            AudioManager.Instance.PlaySFX(sweepSFX);
            isPlaying = true;
        }
    }

    // Menghentikan status suara menyapu
    // Biasanya dipanggil saat animasi atau aksi menyapu selesai
    public void StopSweep()
    {
        isPlaying = false;
    }
}
