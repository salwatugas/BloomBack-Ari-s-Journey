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

    // =========================
    // WARNING UI (ENERGY)
    // =========================
    // Digunakan untuk menampilkan tanda seru
    // saat player mencoba melakukan aksi tetapi energi tidak mencukupi
    public UIWarningIndicator energyWarning;

    // Inisialisasi singleton dan energi awal
    void Awake()
    {
        // Cegah duplikasi EnergyManager
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
    // Mengembalikan false jika energi tidak mencukupi
    public bool UseEnergy(int amount = 1)
    {
        // Jika energi tidak cukup, tampilkan warning
        if (currentEnergy < amount)
        {
            if (energyWarning != null)
                energyWarning.ShowWarning();

            return false;
        }

        // Kurangi energi
        currentEnergy -= amount;
        NotifyUI();

        // Jika energi habis setelah digunakan, tampilkan warning
        if (currentEnergy <= 0)
        {
            if (energyWarning != null)
                energyWarning.ShowWarning();
        }

        return true;
    }

    // Mencatat pembersihan trash untuk aturan pengurangan energi
    public void RegisterTrashCleaned()
    {
        // Jika player mencoba membersihkan saat energi habis,
        // tampilkan warning sebagai feedback visual
        if (!HasEnergy())
        {
            if (energyWarning != null)
                energyWarning.ShowWarning();
            return;
        }

        trashCounter++;

        // Setiap beberapa trash, energi akan berkurang
        if (trashCounter >= trashPerEnergy)
        {
            trashCounter = 0;
            UseEnergy(1);
        }
    }

    // Mencatat pembersihan dust trash untuk aturan pengurangan energi
    public void RegisterDustTrashCleaned()
    {
        // Jika player mencoba membersihkan saat energi habis,
        // tampilkan warning sebagai feedback visual
        if (!HasEnergy())
        {
            if (energyWarning != null)
                energyWarning.ShowWarning();
            return;
        }

        dustTrashCounter++;

        // Setiap beberapa dust trash, energi akan berkurang
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

    // =========================
    // RESET ENERGY (START GAME)
    // =========================
    public void ResetEnergy()
    {
        // Reset energi ke kondisi awal
        currentEnergy = maxEnergy;

        // Reset seluruh counter internal
        trashCounter = 0;
        dustTrashCounter = 0;

        NotifyUI();

        Debug.Log("[RESET] EnergyManager reset");
    }

        // Menampilkan warning energi secara manual
    // Dipanggil oleh sistem lain saat aksi gagal karena energi
    public void ShowEnergyWarning()
    {
        if (energyWarning != null)
            energyWarning.ShowWarning();
    }

}
