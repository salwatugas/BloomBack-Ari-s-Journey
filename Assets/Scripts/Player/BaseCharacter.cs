using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BaseCharacter adalah class dasar untuk semua karakter
// Script ini menyediakan fungsi gerak umum yang bisa digunakan
// oleh player maupun karakter lain
// Konsep OOP yang digunakan adalah Inheritance dan Polymorphism,
// karena class turunan dapat menggunakan atau mengubah perilaku Move()
// Design Pattern yang digunakan adalah Template Method (sederhana)
public class BaseCharacter : MonoBehaviour
{
    // Kecepatan gerak karakter
    public float moveSpeed = 5f;

    // Fungsi gerak dasar karakter
    // Method ini bersifat virtual agar bisa dioverride oleh class turunan
    public virtual void Move(Rigidbody2D rb, Vector2 movement)
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
