using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// EnergyUI bertugas menampilkan jumlah energi player di layar
// Script ini hanya mengatur tampilan UI energi
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang digunakan adalah Singleton dan Observer (manual)
public class EnergyUI : MonoBehaviour
{
    // Instance tunggal EnergyUI
    public static EnergyUI Instance;

    // List icon energi aktif (warna kuning)
    public List<Image> energyFGs;

    void Awake()
    {
        // Pastikan hanya ada satu EnergyUI di runtime
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Sinkronisasi awal dengan EnergyManager
        if (EnergyManager.Instance != null)
            UpdateEnergy(EnergyManager.Instance.currentEnergy);
    }

    // Memperbarui tampilan energi berdasarkan nilai saat ini
    public void UpdateEnergy(int currentEnergy)
    {
        for (int i = 0; i < energyFGs.Count; i++)
        {
            // Aktifkan icon sesuai jumlah energi
            energyFGs[i].enabled = i < currentEnergy;
        }
    }
}
