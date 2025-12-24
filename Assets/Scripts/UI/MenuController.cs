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
        // =========================
        // RESET SEMUA DATA GAME
        // =========================

        // Reset seluruh data runtime berbasis ScriptableObject
        if (GameDataRuntime.Instance != null)
            GameDataRuntime.Instance.ResetAllRuntimeData();

        // Tunggu 1 frame agar runtime data benar-benar dibuat ulang
        yield return null;

        // Reset GameManager (poin & progres area)
        if (GameManager.Instance != null)
            GameManager.Instance.ResetGame();

        // 3️⃣ Reset energi player
        if (EnergyManager.Instance != null)
            EnergyManager.Instance.RestoreEnergy();

        // Reset inventory dekorasi
        if (DecorationInventory.Instance != null)
        {
            DecorationInventory.Instance.ownedDecorations.Clear();
            DecorationInventory.Instance.onInventoryChanged?.Invoke();
        }

        // Reset status placement dekorasi
        if (DecorationPlacementManager.Instance != null)
        {
            DecorationPlacementManager.Instance.CancelPlacement();
            DecorationPlacementManager.Instance.isRemoveMode = false;
        }

        // Reset tool yang sedang digunakan
        if (ToolManager.Instance != null)
            ToolManager.Instance.UnequipTool();

        Debug.Log("[START GAME] Semua data berhasil di-reset");

        // Load scene awal game melalui sistem loading
        LoadingManager.LoadScene("Area_RumahAri");
    }
}
