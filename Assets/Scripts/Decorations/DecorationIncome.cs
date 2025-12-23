using UnityEngine;
using System.Collections;

// DecorationIncome bertugas memberikan poin secara berkala dari dekorasi
// Script ini digunakan pada dekorasi yang sudah dipasang di area
// Konsep OOP yang digunakan adalah Encapsulation,
// karena logika income dan penyimpanan waktunya dikontrol oleh class ini
// Design Pattern yang terlibat adalah Singleton (melalui GameManager)
// serta konsep persistence sederhana untuk menyimpan waktu income berikutnya
public class DecorationIncome : MonoBehaviour
{
    // ID unik untuk dekorasi ini
    public string decorationID;

    // Data penyimpanan waktu income dekorasi
    public DecorationIncomeSaveData saveData;

    // Jumlah poin yang dihasilkan setiap interval
    public int incomeAmount = 1;

    // Interval waktu income (dalam detik)
    public float incomeInterval = 30f;

    // Coroutine income
    Coroutine incomeRoutine;

    // Inisialisasi income saat dekorasi aktif
    void Start()
    {
        // Tentukan waktu income berikutnya
        float nextTime = Time.time + incomeInterval;

        // Cek apakah sudah ada data income sebelumnya
        var saved = saveData.Get(decorationID);
        if (saved != null)
        {
            nextTime = saved.nextIncomeTime;
        }
        else
        {
            // Simpan data pertama kali
            saveData.Save(decorationID, nextTime);
        }

        // Mulai loop income
        StartCoroutine(IncomeLoop(nextTime));
    }

    // Coroutine yang menangani pemberian income secara berkala
    IEnumerator IncomeLoop(float nextIncomeTime)
    {
        while (true)
        {
            // Tunggu hingga waktu income berikutnya
            float wait = nextIncomeTime - Time.time;
            if (wait > 0)
                yield return new WaitForSeconds(wait);

            // Tambahkan poin ke pemain
            if (GameManager.Instance != null)
                GameManager.Instance.AddPoint(incomeAmount);

            // Hitung waktu income selanjutnya
            nextIncomeTime = Time.time + incomeInterval;

            // Simpan waktu income berikutnya
            saveData.Save(decorationID, nextIncomeTime);
        }
    }

    // Hentikan coroutine saat object dihancurkan
    void OnDestroy()
    {
        if (incomeRoutine != null)
            StopCoroutine(incomeRoutine);
    }
}
