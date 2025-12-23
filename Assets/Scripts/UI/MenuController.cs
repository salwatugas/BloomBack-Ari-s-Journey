using UnityEngine;
using System.Collections;

// MenuController bertugas mengatur aksi pada menu utama
// Script ini menangani proses memulai game dari menu
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang digunakan adalah Command (event tombol)
public class MenuController : MonoBehaviour
{
    // Dipanggil saat tombol Start Game ditekan
    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    // Proses memulai game secara bertahap
    IEnumerator StartGameRoutine()
    {
        // Reset semua data runtime agar game mulai dari kondisi awal
        if (GameDataRuntime.Instance != null)
            GameDataRuntime.Instance.ResetAllRuntimeData();

        // Menunggu satu frame agar Unity menyelesaikan update internal
        yield return null;

        // Load scene awal game melalui sistem loading
        LoadingManager.LoadScene("Area_RumahAri");
    }
}
