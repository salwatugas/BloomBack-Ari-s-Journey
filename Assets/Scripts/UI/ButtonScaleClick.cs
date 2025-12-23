using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

// ButtonScaleClick bertugas memberi efek visual
// saat tombol UI diklik oleh player
// Script ini hanya mengatur animasi skala tombol
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang digunakan adalah Command (event klik UI)
public class ButtonScaleClick : MonoBehaviour, IPointerClickHandler
{
    // Skala tombol saat diklik
    public float clickScale = 0.9f;

    // Kecepatan animasi klik
    public float animSpeed = 0.1f;

    // Dipanggil otomatis saat tombol diklik
    public void OnPointerClick(PointerEventData eventData)
    {
        // Pastikan object masih aktif sebelum menjalankan animasi
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ClickAnim());
        }
    }

    // Coroutine animasi klik tombol
    private IEnumerator ClickAnim()
    {
        Vector3 original = transform.localScale;
        Vector3 small = original * clickScale;

        // Tombol mengecil
        transform.localScale = small;

        // Menunggu dengan waktu realtime
        // agar animasi tetap jalan walaupun game di-pause
        yield return new WaitForSecondsRealtime(animSpeed);

        // Tombol kembali ke ukuran normal
        transform.localScale = original;
    }
}
