using UnityEngine;

// EnergyManager bertugas mengatur sistem energi player
// Script ini mengelola penggunaan, pemulihan, dan pengurangan energi
// Konsep OOP yang digunakan adalah Encapsulation,
// karena seluruh aturan energi dibungkus dalam satu class
// Design Pattern yang digunakan adalah Singleton dan Observer (sederhana)
public class EnergyManager : MonoBehaviour
{
    // Instance tunggal EnergyManager
    public static EnergyManager Instance;

    // Jumlah maksimum energi
    public int maxEnergy = 5;

    // Energi saat ini
    public int currentEnergy;

    // Aturan pengurangan energi dari trash
    public int trashPerEnergy = 3;
    private int trashCounter = 0;

    // Aturan pengurangan energi dari dust trash
    public int dustTrashPerEnergy = 2;
    private int dustTrashCounter = 0;

    // Inisialisasi singleton dan energi awal
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Pastikan EnergyManager tidak menjadi child object
        transform.SetParent(null);

        // Energi bersifat global antar scene
        DontDestroyOnLoad(gameObject);

        // Set energi awal penuh
        currentEnergy = maxEnergy;
    }

    // Memberi tahu UI energi agar diperbarui
    void NotifyUI()
    {
        if (EnergyUI.Instance != null)
            EnergyUI.Instance.UpdateEnergy(currentEnergy);
    }

    // Mengurangi energi saat player melakukan aksi
    public bool UseEnergy(int amount = 1)
    {
        if (currentEnergy < amount)
            return false;

        currentEnergy -= amount;
        NotifyUI();

        return true;
    }

    // Mencatat pembersihan trash untuk aturan pengurangan energi
    public void RegisterTrashCleaned()
    {
        if (!HasEnergy())
            return;

        trashCounter++;

        if (trashCounter >= trashPerEnergy)
        {
            trashCounter = 0;
            UseEnergy(1);
        }
    }

    // Mencatat pembersihan dust trash untuk aturan pengurangan energi
    public void RegisterDustTrashCleaned()
    {
        if (!HasEnergy())
            return;

        dustTrashCounter++;

        if (dustTrashCounter >= dustTrashPerEnergy)
        {
            dustTrashCounter = 0;
            UseEnergy(1);
        }
    }

    // Mengembalikan energi penuh saat player tidur
    public void RestoreEnergy()
    {
        currentEnergy = maxEnergy;

        // Reset counter agar tidak carry over
        trashCounter = 0;
        dustTrashCounter = 0;

        NotifyUI();
    }

    // Mengecek apakah player masih memiliki energi
    public bool HasEnergy()
    {
        return currentEnergy > 0;
    }
}
