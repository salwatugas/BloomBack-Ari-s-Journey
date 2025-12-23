using UnityEngine;

// PersistentEventSystem bertugas menjaga EventSystem
// agar tidak hilang saat perpindahan scene
// Script ini digunakan untuk memastikan input UI
// tetap berfungsi di seluruh scene
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang diterapkan secara konseptual adalah Singleton (satu EventSystem aktif)
public class PersistentEventSystem : MonoBehaviour
{
    void Awake()
    {
        // Menjaga GameObject ini tetap ada
        // meskipun scene berpindah
        DontDestroyOnLoad(gameObject);
    }
}
