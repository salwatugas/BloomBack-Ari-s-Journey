using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// LoadingController bertugas mengatur proses loading scene
// Script ini biasanya digunakan di scene loading
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang digunakan adalah State (sederhana)
public class LoadingController : MonoBehaviour
{
    // Waktu tunggu sebelum mulai load scene berikutnya
    public float waitDuration = 5f;

    void Start()
    {
        // Mulai proses loading saat scene loading aktif
        StartCoroutine(LoadNextScene());
    }

    // Coroutine untuk menunggu dan memuat scene tujuan
    private IEnumerator LoadNextScene()
    {
        // Menunggu beberapa detik (misalnya untuk animasi loading)
        yield return new WaitForSeconds(waitDuration);

        // Load scene tujuan secara asynchronous
        AsyncOperation async =
            SceneManager.LoadSceneAsync(LoadingManager.targetScene);

        // Tahan aktivasi scene sampai siap
        async.allowSceneActivation = false;

        // Menunggu hingga loading hampir selesai
        while (!async.isDone)
        {
            if (async.progress >= 0.9f)
            {
                // Aktifkan scene jika sudah siap
                async.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
