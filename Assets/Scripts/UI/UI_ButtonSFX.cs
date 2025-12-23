using UnityEngine;

// UIButtonSFX bertugas memutar sound effect saat tombol UI ditekan
// Script ini digunakan sebagai helper pada Button UI
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang digunakan adalah Command dan Facade
public class UIButtonSFX : MonoBehaviour
{
    // Audio clip untuk suara klik tombol
    public AudioClip clickSound;

    // Dipanggil oleh event OnClick pada Button
    public void PlayClick()
    {
        if (AudioManager.Instance != null && clickSound != null)
        {
            AudioManager.Instance.PlaySFX(clickSound);
        }
    }
}
