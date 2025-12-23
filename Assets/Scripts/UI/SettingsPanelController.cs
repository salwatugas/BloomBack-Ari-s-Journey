using UnityEngine;

// SettingsPanelController bertugas mengatur buka dan tutup panel Settings
// Script ini hanya menangani visibilitas UI panel
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang digunakan adalah Command dan State (implisit)
public class SettingsPanelController : MonoBehaviour
{
    // Panel UI Settings
    public GameObject settingsPanel;

    void Start()
    {
        // Panel settings ditutup saat awal
        settingsPanel.SetActive(false);
    }

    // Membuka panel settings
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    // Menutup panel settings
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
}
