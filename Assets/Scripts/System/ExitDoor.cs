using UnityEngine;

// ExitDoor bertugas mengatur interaksi pintu keluar
// Script ini memungkinkan player berpindah scene dengan transisi fade
// Konsep OOP yang digunakan adalah Encapsulation,
// karena logika pintu keluar dibungkus dalam satu class
// Design Pattern yang digunakan adalah Command (sederhana)
// serta Singleton secara tidak langsung melalui ScreenFader
public class ExitDoor : MonoBehaviour
{
    // Nama scene tujuan
    public string targetScene;

    // Durasi fade khusus untuk pintu ini
    public float fadeDuration = 1.5f;

    // Penanda apakah player berada di area pintu
    private bool playerInRange = false;

    // Mengecek input keluar setiap frame
    void Update()
    {
        if (!playerInRange) return;

        // Tekan E untuk keluar
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Set durasi fade khusus untuk transisi ini
            ScreenFader.Instance.SetFadeDuration(fadeDuration);

            // Panggil fungsi fade dan load scene
            ScreenFader.Instance.FadeOutAndLoadScene(targetScene);
        }
    }

    // Mendeteksi player masuk ke area pintu
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    // Mendeteksi player keluar dari area pintu
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
