using UnityEngine;

// Script ini bertugas mendeteksi objek interaksi terdekat
// dan mengatur kemunculan icon interaksi (icon E)
// Konsep OOP yang digunakan:
// - Single Responsibility Principle:
//   Class ini hanya fokus pada deteksi interaksi dan UI icon
// - Encapsulation:
//   Seluruh logika tampil / hilang icon dibungkus dalam satu class
// - Polymorphism (konseptual):
//   Satu detector dapat menangani berbagai tipe objek interaksi

public class InteractionDetector : MonoBehaviour
{
    // Jarak maksimal untuk mendeteksi objek interaksi
    public float detectRange = 1.2f;

    // Prefab icon interaksi (icon E)
    public InteractionIconUI iconPrefab;

    // Instance icon yang digunakan saat runtime
    InteractionIconUI iconInstance;

    // Target interaksi terdekat
    Transform currentTarget;

    // Inisialisasi icon saat game dimulai
    void Start()
    {
        iconInstance = Instantiate(iconPrefab);
        iconInstance.Hide();
    }

    void Update()
    {
        // Deteksi semua collider di sekitar player
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(transform.position, detectRange);

        Transform nearest = null;
        float minDistance = Mathf.Infinity;

        // Cari objek interaksi terdekat
        foreach (var hit in hits)
        {
            if (!hit.CompareTag("Interactable"))
                continue;

            float dist = Vector2.Distance(
                transform.position,
                hit.transform.position
            );

            if (dist < minDistance)
            {
                minDistance = dist;
                nearest = hit.transform;
            }
        }

        currentTarget = nearest;

        // Jika tidak ada target, sembunyikan icon
        if (currentTarget == null)
        {
            iconInstance.Hide();
            return;
        }

        bool isCompleted = false;
        bool isInteracting = false;

        // Cek status interaksi berdasarkan tipe objek
        if (currentTarget.TryGetComponent(out SeedPlant seed))
        {
            isCompleted = seed.IsInteractionCompleted();
            isInteracting = seed.IsInteracting();
        }
        else if (currentTarget.TryGetComponent(out Trash trash))
        {
            isCompleted = trash.IsInteractionCompleted();
            isInteracting = trash.IsInteracting();
        }
        else if (currentTarget.TryGetComponent(out DustTrash dust))
        {
            isCompleted = dust.IsInteractionCompleted();
            isInteracting = dust.IsInteracting();
        }

        // Jika interaksi sudah selesai → icon hilang permanen
        if (isCompleted)
        {
            iconInstance.Hide();
            return;
        }

        // Jika sedang proses hold → icon disembunyikan sementara
        if (isInteracting)
        {
            iconInstance.Hide();
            return;
        }

        // Tampilkan icon di posisi anchor (jika ada)
        Transform anchor = currentTarget.Find("IconAnchor");
        Vector3 pos = anchor != null
            ? anchor.position
            : currentTarget.position;

        iconInstance.Show(pos);
    }

    // Visualisasi area deteksi di editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
