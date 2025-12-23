using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// CustomToggle adalah toggle UI versi custom
// Script ini digunakan untuk mengatur ON/OFF setting tertentu
// seperti musik atau sound effect
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang digunakan adalah Observer melalui UnityEvent
public class CustomToggle : MonoBehaviour
{
    // Background UI untuk kondisi ON dan OFF
    public GameObject bgOn;
    public GameObject bgOff;

    // Knob toggle yang bergerak
    public RectTransform knob;

    // Posisi knob saat ON dan OFF
    public Vector2 knobOnPos;
    public Vector2 knobOffPos;

    // Event yang dipanggil saat toggle berubah
    public UnityEvent<bool> OnToggleChanged;

    // Key unik untuk menyimpan state toggle
    public string toggleKey = "Toggle_Default";

    // Status toggle saat ini
    private bool isOn = true;

    void Start()
    {
        // Load state toggle dari PlayerPrefs
        isOn = PlayerPrefs.GetInt(toggleKey, 1) == 1;

        // Terapkan visual awal
        UpdateToggleUI();

        // Daftarkan event klik pada Button
        GetComponent<Button>().onClick.AddListener(Toggle);
    }

    // Mengubah status toggle
    void Toggle()
    {
        isOn = !isOn;

        // Simpan state toggle
        PlayerPrefs.SetInt(toggleKey, isOn ? 1 : 0);

        // Update tampilan UI
        UpdateToggleUI();

        // Beri tahu sistem lain bahwa toggle berubah
        OnToggleChanged.Invoke(isOn);
    }

    // Mengatur tampilan UI berdasarkan status toggle
    void UpdateToggleUI()
    {
        bgOn.SetActive(isOn);
        bgOff.SetActive(!isOn);

        knob.anchoredPosition = isOn ? knobOnPos : knobOffPos;
    }
}
