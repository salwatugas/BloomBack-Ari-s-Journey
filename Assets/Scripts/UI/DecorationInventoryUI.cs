using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// DecorationInventoryUI bertugas menampilkan isi inventory dekorasi
// Script ini hanya mengatur UI slot dekorasi dan event kliknya
// Konsep OOP yang digunakan adalah Encapsulation
// Design Pattern yang digunakan adalah Observer dan Command (UI event)
public class DecorationInventoryUI : MonoBehaviour
{
    // Daftar slot UI untuk dekorasi (maksimal 3 slot)
    public List<Button> decorationSlots;

    void Start()
    {
        // Berlangganan ke event perubahan inventory
        DecorationInventory.Instance.onInventoryChanged += RefreshUI;

        // Update tampilan awal
        RefreshUI();
    }

    // Memperbarui tampilan slot dekorasi sesuai data inventory
    void RefreshUI()
    {
        var items = DecorationInventory.Instance.ownedDecorations;

        for (int i = 0; i < decorationSlots.Count; i++)
        {
            if (i < items.Count)
            {
                // Ambil data dekorasi berdasarkan ID
                string itemID = items[i];
                var data = DecorationDatabase.Instance.Get(itemID);

                // Set icon dan aktifkan slot
                decorationSlots[i].image.sprite = data.icon;
                decorationSlots[i].gameObject.SetActive(true);

                // Pastikan tidak ada listener lama
                decorationSlots[i].onClick.RemoveAllListeners();

                // Saat slot diklik, pilih dekorasi untuk placement
                decorationSlots[i].onClick.AddListener(() =>
                {
                    DecorationPlacementManager.Instance
                        .SelectDecoration(itemID, data.prefab);
                });
            }
            else
            {
                // Slot kosong disembunyikan
                decorationSlots[i].gameObject.SetActive(false);
            }
        }
    }
}
