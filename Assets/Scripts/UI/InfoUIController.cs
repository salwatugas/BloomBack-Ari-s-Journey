using UnityEngine;
using UnityEngine.UI;

// InfoUIController bertugas mengatur panel informasi atau tutorial
// Script ini mengelola tampilan slide info dan navigasinya
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang digunakan adalah Singleton dan State (sederhana)
public class InfoUIController : MonoBehaviour
{
    // Instance tunggal InfoUIController
    public static InfoUIController Instance;

    // Panel utama info
    public GameObject infoPanel;

    // Image untuk menampilkan slide
    public Image infoImage;

    // Daftar sprite slide informasi
    public Sprite[] infoSprites;

    // Index slide yang sedang ditampilkan
    int currentIndex = 0;

    void Awake()
    {
        // Pastikan hanya ada satu InfoUIController
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
        // Panel info disembunyikan di awal
        if (infoPanel != null)
            infoPanel.SetActive(false);
    }

    // Membuka panel info dan menghentikan waktu game
    public void OpenInfo()
    {
        currentIndex = 0;
        UpdateImage();
        infoPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // Menutup panel info dan melanjutkan waktu game
    public void CloseInfo()
    {
        infoPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // Pindah ke slide berikutnya
    public void Next()
    {
        if (currentIndex < infoSprites.Length - 1)
        {
            currentIndex++;
            UpdateImage();
        }
    }

    // Kembali ke slide sebelumnya
    public void Prev()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateImage();
        }
    }

    // Mengupdate sprite sesuai index slide
    void UpdateImage()
    {
        if (infoImage != null && infoSprites.Length > 0)
            infoImage.sprite = infoSprites[currentIndex];
    }
}
