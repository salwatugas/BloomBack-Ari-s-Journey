using UnityEngine;

// YSort bertugas mengatur urutan render sprite berdasarkan posisi Y
// Script ini sangat penting untuk objek tinggi seperti DEKORASI LAMPU
// karena lampu memiliki tinggi sprite yang panjang
// Posisi Y lampu sangat mempengaruhi apakah dekorasi lain tampil
// di depan atau di belakang lampu
//
// Contoh kasus:
// - Dekorasi di depan lampu harus terlihat MENUTUPI bagian bawah lampu
// - Dekorasi di belakang lampu harus terlihat TERTUTUP oleh lampu
//
// Tanpa YSort, urutan render lampu dan dekorasi lain akan terlihat salah
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang digunakan adalah Component-based design (Unity)
[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    // Offset tambahan untuk objek tinggi seperti lampu atau pohon
    // Offset membantu menyesuaikan urutan render bagian atas sprite
    public int offset = 0;

    // Komponen SpriteRenderer yang akan diatur sorting order-nya
    SpriteRenderer sr;

    // Ambil SpriteRenderer saat object aktif
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Sorting dilakukan di LateUpdate agar posisi object sudah final
    void LateUpdate()
    {
        // Semakin ke bawah posisi object, semakin besar sorting order
        // Ini membuat objek di bawah terlihat di depan objek lain
        sr.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100) + offset;
    }
}
