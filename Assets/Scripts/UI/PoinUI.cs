using TMPro;
using UnityEngine;

// PointUI bertugas menampilkan jumlah poin player di layar
// Script ini hanya membaca data poin dari GameManager
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang digunakan adalah Observer (manual) dan MVC (konseptual)
public class PointUI : MonoBehaviour
{
    // Text UI untuk menampilkan poin
    public TextMeshProUGUI pointText;

    void Update()
    {
        // Update tampilan poin setiap frame
        if (GameManager.Instance != null)
        {
            pointText.text = " " + GameManager.Instance.GetScore();
        }
    }
}
