using UnityEngine;
using UnityEngine.EventSystems;

// Script ini memastikan hanya ada SATU EventSystem
// Jika ada duplikat, otomatis dihancurkan
public class EventSystemGuard : MonoBehaviour
{
    void Awake()
    {
        if (FindObjectsOfType<EventSystem>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
