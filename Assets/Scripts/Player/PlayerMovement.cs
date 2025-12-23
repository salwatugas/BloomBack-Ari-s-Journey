using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// PlayerMovement bertugas mengatur pergerakan dan animasi player
// Script ini merupakan turunan dari BaseCharacter
// Konsep OOP yang digunakan adalah Inheritance dan Polymorphism
// PlayerMovement fokus pada input dan animasi,
// sedangkan logika gerak dasar diambil dari BaseCharacter
public class PlayerMovement : BaseCharacter
{
    // Rigidbody player untuk pergerakan fisik
    public Rigidbody2D rb;

    // Animator untuk animasi berjalan dan idle
    public Animator animator;

    // Menyimpan arah input
    Vector2 movement;

    void Update()
    {
        // Jika mouse sedang berinteraksi dengan UI,
        // input keyboard diabaikan agar player tidak bergerak
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            movement = Vector2.zero;
            animator.SetBool("isWalking", false);
            return;
        }

        // Ambil input horizontal dan vertical
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Cek apakah player sedang bergerak
        bool isMoving = movement.sqrMagnitude > 0;

        // Set parameter animasi berjalan
        animator.SetBool("isWalking", isMoving);

        // Set parameter blend tree arah gerak
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        // Menyimpan arah terakhir hanya saat bergerak
        // agar animasi idle menghadap arah terakhir
        if (isMoving)
        {
            animator.SetFloat("LastHorizontal", movement.x);
            animator.SetFloat("LastVertical", movement.y);
        }
    }

    void FixedUpdate()
    {
        // Jika ada input gerak, gunakan fungsi Move dari BaseCharacter
        if (movement.sqrMagnitude > 0)
        {
            Move(rb, movement);
        }
        else
        {
            // Menghentikan Rigidbody secara eksplisit
            // untuk mencegah efek sliding atau snapping
            rb.velocity = Vector2.zero;
        }
    }
}
