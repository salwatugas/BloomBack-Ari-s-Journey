using UnityEngine;
using System.Collections;

// UIWarningIndicator bertugas menampilkan indikator tanda seru
// dengan animasi getar sebagai feedback kesalahan aksi player
// Script ini reusable untuk berbagai icon UI (energi, sapu, siram)
// Konsep OOP: Encapsulation
public class UIWarningIndicator : MonoBehaviour
{
    [Header("Shake Settings")]
    public float shakeDuration = 0.3f;
    public float shakeStrength = 8f;

    [Header("Auto Hide")]
    public float visibleTime = 0.5f;

    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private Coroutine shakeRoutine;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;

        // Pastikan icon tidak tampil saat awal
        gameObject.SetActive(false);
    }

    // Dipanggil saat terjadi aksi SALAH
    public void ShowWarning()
    {
        // Reset jika sedang animasi
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        gameObject.SetActive(true);
        rectTransform.anchoredPosition = originalPosition;

        shakeRoutine = StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offset = Mathf.Sin(elapsed * 50f) * shakeStrength;
            rectTransform.anchoredPosition =
                originalPosition + new Vector3(offset, 0f, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = originalPosition;

        yield return new WaitForSeconds(visibleTime);

        gameObject.SetActive(false);
        shakeRoutine = null;
    }
}
