using UnityEngine;
using UnityEngine.SceneManagement;

// Script ini bertugas:
// 1. Menjaga hanya SATU instance aktif (guard / anti double)
// 2. Menjadikan object DontDestroyOnLoad
// 3. Menghancurkan object saat masuk ke scene tertentu (Main Menu / Loading)
public class DestroyOnScene : MonoBehaviour
{
    private static DestroyOnScene instance;

    [Header("Destroy When Enter Scene")]
    [Tooltip("GameObject akan dihancurkan jika masuk ke scene ini")]
    public string[] destroyOnScenes;

    void Awake()
    {
        // =========================
        // GUARD (CEGAH DOUBLE)
        // =========================
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // =========================
        // PERSISTENT OBJECT
        // =========================
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (string sceneName in destroyOnScenes)
        {
            if (scene.name == sceneName)
            {
                // reset instance biar bisa spawn ulang di gameplay
                instance = null;
                Destroy(gameObject);
                return;
            }
        }
    }
}
