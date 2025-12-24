using UnityEngine;

// AreaProgressData digunakan untuk menyimpan data progres suatu area
// seperti jumlah sampah, tanaman, dan kondisi lingkungan
// Konsep OOP yang digunakan adalah Encapsulation,
// karena seluruh data progres dibungkus dalam satu class khusus
// Design Pattern yang digunakan adalah ScriptableObject,
// sehingga data dapat dipisahkan dari logic gameplay
[CreateAssetMenu(menuName = "Game Data/Area Progress")]
public class AreaProgressData : ScriptableObject
{
    // Total jumlah sampah di area
    public int totalTrash;

    // Jumlah sampah yang sudah dibersihkan
    public int trashCleaned;

    // Total jumlah tanaman di area
    public int totalSeed;

    // Jumlah tanaman yang sudah tumbuh
    public int seedGrown;

    // Status apakah lingkungan sudah bersih sepenuhnya
    public bool isEnvironmentClean;

    // Mengatur ulang seluruh data progres area
    public void ResetData()
    {
        totalTrash = 0;
        trashCleaned = 0;
        totalSeed = 0;
        seedGrown = 0;
        isEnvironmentClean = false;

        Debug.Log("[RESET] AreaProgressData reset");
    }
}
