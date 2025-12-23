using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

// ScreenFader bertugas mengatur transisi fade layar
// Script ini digunakan untuk perpindahan scene dan perubahan state visual
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang digunakan adalah Singleton dan Facade
public class ScreenFader : MonoBehaviour
{
    // Instance tunggal ScreenFader
    public static ScreenFader Instance;

    // CanvasGroup untuk mengatur transparansi layar
    [SerializeField] private CanvasGroup canvasGroup;

    // Durasi default fade
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        // Pastikan hanya ada satu ScreenFader di runtime
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Fade layar menjadi hitam
    public async Task FadeOut()
    {
        await Fade(1f);
    }

    // Fade layar menjadi transparan
    public async Task FadeIn()
    {
        await Fade(0f);
    }

    // Proses fade inti berdasarkan target alpha
    private async Task Fade(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha =
                Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            await Task.Yield();
        }

        canvasGroup.alpha = targetAlpha;
    }

    // Mengubah durasi fade secara dinamis
    public void SetFadeDuration(float duration)
    {
        fadeDuration = duration;
    }

    // Digunakan untuk exit door atau perpindahan scene langsung
    public async void FadeOutAndLoadScene(string sceneName)
    {
        await FadeOut();
        SceneManager.LoadScene(sceneName);
        await Task.Yield(); // tunggu satu frame
        await FadeIn();
    }
}
