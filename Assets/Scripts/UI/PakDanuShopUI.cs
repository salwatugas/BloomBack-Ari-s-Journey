using UnityEngine;

public class PakDanuShopUI : MonoBehaviour
{
    public static PakDanuShopUI Instance;
    void Start()
{
    gameObject.SetActive(false);
}

    void Awake()
    {
        Instance = this;
    }

    public void OpenShop()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f; // ⏸ pause game
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f; // ▶ resume game
    }
}
