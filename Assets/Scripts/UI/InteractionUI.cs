using UnityEngine;

// Script ini bertugas mengatur tampilan icon interaksi (icon E)
// Class ini TIDAK mengandung logika gameplay sama sekali
// seperti input, deteksi jarak, atau progres interaksi
//
// Konsep OOP yang digunakan:
// - Single Responsibility Principle:
//   Script ini hanya fokus pada urusan visual icon
// - Encapsulation:
//   Logika menampilkan dan menyembunyikan icon dibungkus dalam satu class
public class InteractionIconUI : MonoBehaviour
{
    // Menampilkan icon di posisi world tertentu
    // Biasanya dipanggil saat player berada dekat objek interaksi
    public void Show(Vector3 worldPosition)
    {
        transform.position = worldPosition;
        gameObject.SetActive(true);
    }

    // Menyembunyikan icon
    // Dipanggil saat player menjauh atau interaksi sudah selesai
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
