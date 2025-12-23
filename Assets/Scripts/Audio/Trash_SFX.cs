using UnityEngine;

// Script ini bertugas memutar sound effect saat player mengambil atau membersihkan sampah
// Konsep OOP yang digunakan adalah Encapsulation,
// karena script ini hanya mengurus satu jenis suara tertentu
// Design Pattern yang terlibat adalah Singleton melalui AudioManager
public class TrashSFX : MonoBehaviour
{
    // Sound effect yang dimainkan saat sampah diambil
    public AudioClip trashPickupSFX;

    // Memutar sound effect pengambilan sampah
    public void PlayPickup()
    {
        // Cek keamanan sebelum memutar suara
        if (AudioManager.Instance != null && trashPickupSFX != null)
        {
            AudioManager.Instance.PlaySFX(trashPickupSFX);
        }
    }
}
