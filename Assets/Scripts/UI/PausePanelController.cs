using UnityEngine;

// PauseUI bertugas mengatur tampilan dan logika pause game
// Script ini hanya mengelola UI pause dan waktu game
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang digunakan adalah State dan Command
public class PauseUI : MonoBehaviour
{
    // Panel UI pause
    public GameObject kotakPause;

    // Penanda apakah game sedang pause
    private bool isPaused = false;

    void Start()
    {
        // Pause UI disembunyikan saat awal
        kotakPause.SetActive(false);
    }

    // Toggle kondisi pause (pause / resume)
    public void TogglePause()
    {
        isPaused = !isPaused;
        ApplyPauseState();
    }

    // Menutup pause secara paksa (resume game)
    public void ClosePause()
    {
        isPaused = false;
        ApplyPauseState();
    }

    // Menerapkan kondisi pause ke UI dan waktu game
    void ApplyPauseState()
    {
        kotakPause.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
