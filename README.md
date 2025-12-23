# BloomBack: Ari’s Journey

**BloomBack: Ari’s Journey** adalah game edukasi bertema kepedulian lingkungan. Pemain akan membantu seorang anak bernama Ari untuk membersihkan area, merawat lingkungan, dan menata dekorasi agar lingkungan kembali asri. Proyek ini dibuat menggunakan Unity sebagai bagian dari pengembangan media pembelajaran.

---

## 1. Progress Commit Pertama

Pada commit pertama, perkembangan yang telah dicapai adalah sebagai berikut:

* Karakter utama (Ari) dapat bergerak ke empat arah: atas, bawah, kiri, dan kanan.
* Sistem movement menggunakan Rigidbody2D dan input keyboard.
* Scene dasar dibuat untuk kebutuhan uji coba pergerakan karakter.

Fitur inti lainnya seperti sistem pembersihan, dekorasi, dan interaksi belum diimplementasikan pada tahap ini.

---

## 2. High Concept

“Membantu Ari memulihkan kebersihan dan keindahan lingkungan melalui aktivitas sederhana seperti membersihkan, merawat, dan menata kembali area sekitar.”

Genre: Simulation, Casual
Platform: PC (Windows)

---

## 3. Fitur Utama (Mengacu pada GDD)

Walaupun belum diterapkan pada commit awal, fitur lengkap yang direncanakan meliputi:

### 3.1 Cleaning System

* Setiap area memiliki persentase kebersihan.
* Pemain dapat melakukan aktivitas seperti mengambil sampah, menyapu, dan menyiram tanaman.
* Masing-masing tindakan meningkatkan progres kebersihan area.

### 3.2 Decoration System

* Pemain dapat menempatkan dekorasi seperti tanaman, bangku, lampu, dan elemen lingkungan lainnya.
* Dekorasi memiliki level dan memberikan poin secara berkala.

### 3.3 Tools Leveling

* Alat seperti sapu dan teko air dapat ditingkatkan levelnya agar lebih efisien.

### 3.4 Energy System

* Ari memiliki energi yang berkurang saat melakukan aktivitas.
* Energi dapat dipulihkan melalui istirahat atau penggunaan item.

### 3.5 NPC Interaction

* NPC memiliki tingkat kenyamanan atau hubungan dengan Ari.
* Hubungan meningkat apabila lingkungan membaik.
* NPC dapat memberikan bantuan atau item dekorasi tertentu.

---

## 4. Tools dan Software

Pengembangan dilakukan menggunakan perangkat berikut:

* Unity Engine
* C#
* Clip Studio Paint (CSP) untuk pembuatan grafis pixel art
* Visual Studio Code
* Git dan GitHub untuk version control

---

## 5. Struktur Folder (Commit Pertama)

```
Assets/
 ├── Scripts/
 │    └── PlayerMovement.cs
 ├── Sprites/
 └── Scenes/
      └── MovementTest.unity

ProjectSettings/
Packages/
.gitignore
README.md
```

---

## 6. Tim Pengembang

Kelompok 2 – Pendidikan Multimedia UPI:

* Kiki Kurniawan (2402777)
* Salwa Nabilah (2403268)
* Gina Amini Nurul Aulia (2405699)

---

## 7. Catatan

Proyek masih berada pada tahap awal. Commit pertama difokuskan pada implementasi sistem movement. Fitur lain akan ditambahkan secara bertahap sesuai dokumen GDD.
