using UnityEngine;
using System.Threading.Tasks;

// BedSleep bertugas mengatur interaksi tidur pada kasur
// Script ini memungkinkan player memulihkan energi dengan tidur
// Konsep OOP yang digunakan adalah Encapsulation,
// karena seluruh logika tidur dibungkus dalam satu class
// Design Pattern yang terlibat adalah State (sederhana)
// serta Singleton secara tidak langsung melalui manager global
public class BedSleep : MonoBehaviour
{
    // Penanda apakah player berada di area kasur
    private bool playerInRange = false;

    // Penanda agar proses tidur tidak bisa dipicu berulang
    private bool isSleeping = false;

    // Mengecek input tidur setiap frame
    void Update()
    {
        // Jika player tidak di area kasur atau sedang tidur, hentikan proses
        if (!playerInRange || isSleeping)
            return;

        // Player tidak bisa tidur jika energi sudah penuh
        if (EnergyManager.Instance != null &&
            EnergyManager.Instance.currentEnergy >= EnergyManager.Instance.maxEnergy)
            return;

        // Tekan E untuk tidur
        if (Input.GetKeyDown(KeyCode.E))
        {
            Sleep();
        }
    }

    // Proses tidur dengan transisi layar
    async void Sleep()
    {
        isSleeping = true;

        // Fade layar ke gelap
        if (ScreenFader.Instance != null)
            await ScreenFader.Instance.FadeOut();

        // Pulihkan energi player hingga penuh
        if (EnergyManager.Instance != null)
            EnergyManager.Instance.RestoreEnergy();

        // Delay kecil agar transisi terasa natural
        await Task.Delay(500);

        // Fade layar kembali terang
        if (ScreenFader.Instance != null)
            await ScreenFader.Instance.FadeIn();

        isSleeping = false;
    }

    // Mendeteksi player masuk ke area kasur
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    // Mendeteksi player keluar dari area kasur
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
