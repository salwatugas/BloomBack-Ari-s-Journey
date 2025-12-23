// Interface ini digunakan sebagai kontrak
// untuk objek yang memiliki proses interaksi berbasis waktu
// seperti menahan tombol (hold E)

// Konsep OOP yang digunakan:
// - Abstraction:
//   Interface ini menyembunyikan detail implementasi
//   dari sistem deteksi interaksi
// - Polymorphism:
//   Banyak tipe objek (Seed, Trash, Dust)
//   dapat diperlakukan sama oleh InteractionDetector
//
// Interface ini digunakan langsung oleh InteractionDetector
// untuk menentukan apakah icon interaksi
// perlu ditampilkan atau disembunyikan
public interface IInteractableProgress
{
    // Mengembalikan true jika interaksi pada objek
    // telah benar-benar selesai
    // (misalnya sampah sudah bersih atau tanaman sudah disiram)
    bool IsInteractionCompleted();
}
