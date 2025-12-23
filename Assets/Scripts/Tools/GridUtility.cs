using UnityEngine;

// GridManager bertugas mengatur sistem grid di dalam game
// Script ini digunakan untuk melakukan snap posisi ke grid
// dan menampilkan visual grid di editor
// Konsep OOP yang digunakan adalah Encapsulation,
// karena seluruh perhitungan grid dikontrol oleh class ini
// Design Pattern yang digunakan adalah Singleton
public class GridUtility : MonoBehaviour
{
    // Instance tunggal GridManager
    public static GridUtility Instance;

    // Ukuran satu sel grid
    public float gridSize = 1f;

    // Offset grid sebagai titik awal (origin)
    public Vector2 gridOffset;

    // Inisialisasi instance grid
    void Awake()
    {
        Instance = this;
    }

    // Mengubah posisi world menjadi posisi yang tersnap ke grid
    public Vector2 SnapPosition(Vector2 worldPos)
    {
        float x = Mathf.Round((worldPos.x - gridOffset.x) / gridSize) * gridSize + gridOffset.x;
        float y = Mathf.Round((worldPos.y - gridOffset.y) / gridSize) * gridSize + gridOffset.y;
        return new Vector2(x, y);
    }

    // Menampilkan grid di editor untuk membantu penempatan objek
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 1f, 0.2f);

        for (int x = -20; x <= 20; x++)
        {
            for (int y = -20; y <= 20; y++)
            {
                Vector3 pos = new Vector3(
                    x * gridSize + gridOffset.x,
                    y * gridSize + gridOffset.y,
                    0
                );

                Gizmos.DrawWireCube(pos, Vector3.one * gridSize);
            }
        }
    }
}
