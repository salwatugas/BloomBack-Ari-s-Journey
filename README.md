# BloomBack: Ari’s Journey

BloomBack: Ari’s Journey adalah game edukasi lingkungan berbasis 2D yang dikembangkan menggunakan **Unity** dan **C#**.  
Game ini mengajak pemain untuk memahami pentingnya menjaga lingkungan melalui aktivitas membersihkan area, menyiram tanaman, dan mendekorasi lingkungan.

Proyek ini dikembangkan sebagai tugas akhir mata kuliah **Pemrograman Berorientasi Objek (PBO)**.

---

## Gameplay Overview

Alur gameplay utama dalam game ini adalah sebagai berikut:

- Pemain masuk ke **Main Menu** dan memilih tombol **Start**
- Pemain memulai permainan di **environment kotor**
- Pemain harus membersihkan seluruh **sampah dan debu**
- Setelah lingkungan bersih, area berubah menjadi **environment cerah**
- Pemain dapat menyiram tanaman dengan dua fase pertumbuhan:
  - Bibit
  - Tumbuh sedang  
  hingga akhirnya menjadi **tumbuh tinggi**
- Penyiraman hanya dapat dilakukan pada fase **bibit** dan **tumbuh sedang**
- Setelah itu pemain dapat berinteraksi dengan **NPC Pak Danu** untuk membeli item dekorasi
- Item dekorasi dapat:
  - Dipasang di **Deco Spot**
  - Dihapus kembali menggunakan ikon sampah
- Aktivitas membersihkan, menyiram, dan memasang dekorasi akan menambah **poin**
- Item dekorasi menghasilkan poin secara **berkala** setelah dipasang

---

## Core Mechanics

### Cleaning System
Pemain membersihkan sampah dan debu menggunakan sistem **hold interaction**.

### Energy System
Beberapa aktivitas mengurangi energi pemain dan harus dikelola dengan baik.

### Plant Growth System
Tanaman memiliki beberapa tahap pertumbuhan yang menentukan apakah tanaman masih bisa disiram.

### Decoration System
Pemain dapat membeli, memasang, dan menghapus dekorasi untuk meningkatkan progres area.

### Scoring System
Poin diperoleh dari aktivitas pemain dan digunakan sebagai mata uang untuk membeli dekorasi.

---

## Cara Bermain

1. **Mulai Permainan**  
   Pemain memulai permainan dari Main Menu dengan menekan tombol *Start*.  
   Player akan langsung berada di area rumah Ari dalam kondisi lingkungan kotor.

2. **Membersihkan Lingkungan**  
   Pemain harus membersihkan seluruh sampah dan debu yang ada di area.
   - Mendekati objek interaksi akan menampilkan ikon tombol **E**.
   - Tahan tombol **E** untuk membersihkan sampah atau debu hingga progress penuh.
   - Sampah dan debu yang berhasil dibersihkan akan menambah poin.

3. **Perubahan Lingkungan**  
   Setelah seluruh sampah dan debu dibersihkan:
   - Lingkungan akan berubah menjadi kondisi cerah.
   - Pemain baru dapat melakukan interaksi lanjutan.

4. **Menyiram Tanaman**  
   Pemain dapat menyiram tanaman menggunakan alat penyiram.
   - Tanaman memiliki beberapa tahap pertumbuhan: **Bibit → Tumbuh Sedang → Tumbuh Tinggi**.
   - Penyiraman hanya bisa dilakukan pada tahap Bibit dan Tumbuh Sedang.
   - Setiap penyiraman membutuhkan energi dan memberikan poin.

5. **Berinteraksi dengan NPC Pak Danu**  
   Setelah lingkungan bersih dan tanaman tumbuh:
   - Pemain dapat menemui NPC Pak Danu.
   - Pemain dapat membeli item dekorasi menggunakan poin yang telah dikumpulkan.

6. **Menempatkan Dekorasi**  
   Item dekorasi yang dibeli dapat ditempatkan di area yang telah disediakan.
   - Setiap dekorasi memberikan tambahan poin secara berkala.
   - Dekorasi juga berkontribusi terhadap progres area.
   - Dekorasi dapat dihapus kembali menggunakan ikon sampah.

7. **Manajemen Energi dan Progres**  
   - Setiap aktivitas tertentu akan mengurangi energi pemain.
   - Pemain dapat kembali ke rumah Ari untuk beristirahat.
   - Progres permainan disimpan secara runtime selama permainan berlangsung.


---

## Object-Oriented Programming (OOP)

### Encapsulation
Setiap objek interaksi seperti **Trash**, **DustTrash**, dan **SeedPlant** mengelola data dan prosesnya sendiri melalui method yang terkontrol.

### Inheritance
Class **PlayerMovement** mewarisi **BaseCharacter** untuk mengimplementasikan sistem pergerakan karakter.

### Polymorphism
Objek interaksi memiliki perilaku berbeda namun dapat diproses menggunakan sistem interaksi yang sama.

### Abstraction
Interface digunakan untuk merepresentasikan objek yang memiliki proses interaksi tanpa bergantung pada detail implementasi.

---

## Design Pattern

### Singleton Pattern
Digunakan pada class manager seperti:
- GameManager
- EnergyManager
- AudioManager
- MusicManager  

Pattern ini memastikan hanya ada satu instance yang mengatur sistem utama game.

### State Pattern
Digunakan pada sistem pertumbuhan tanaman untuk mengatur perubahan perilaku berdasarkan tahap pertumbuhan.

---

## Project Structure

Struktur folder pada proyek ini disusun berdasarkan fungsi dan tanggung jawab masing-masing sistem sebagai berikut:

- **Audio**  
  Berisi script dan asset audio seperti musik gameplay, musik main menu, serta sound effect interaksi (trash, dust trash, watering).

- **Data**  
  Menyimpan script ScriptableObject dan data runtime seperti progres area, progres tanaman, dan data dekorasi.

- **Decorations**  
  Mengatur seluruh sistem dekorasi, mulai dari inventory dekorasi, database item, penempatan, penghapusan, hingga income dekorasi.

- **InteractableObject**  
  Berisi objek-objek yang dapat diinteraksikan secara langsung oleh player, seperti:
  - Trash
  - DustTrash
  - SeedPlant

- **Interaction**  
  Mengatur sistem interaksi player dengan object, termasuk input interaksi, deteksi object, dan pengelolaan alat (tool).

- **Managers**  
  Berisi manager utama yang mengatur sistem global game, seperti:
  - GameManager
  - AudioManager
  - MusicManager

- **NPC**  
  Berisi script untuk karakter NPC, termasuk interaksi dengan Pak Danu sebagai penjual dekorasi.

- **Player**  
  Mengatur karakter player, termasuk pergerakan dan struktur dasar karakter.

- **System**  
  Berisi sistem inti game seperti:
  - Energy System
  - Environment System
  - Save Runtime Data
  - Scene Loading
  - Exit Door
  - Interaction Detector

- **Tools**  
  Berisi utility dan helper script seperti pengaturan sorting, grid, dan event system.

- **UI**  
  Mengatur seluruh tampilan antarmuka pengguna seperti:
  - Menu
  - Map
  - Pause
  - Shop
  - Energy UI
  - Interaction UI


---

## Tools and Technology

- Unity 2D
- C#
- Git & GitHub
- Visual Studio Code

---

## Developer

Kiki Kurniawan 		
Salwa Nabilah 		
Gina Amini Nurul Aulia 	

---

![image alt](https://github.com/salwatugas/BloomBack-Ari-s-Journey/blob/895bfd31f96e1ad35e1b4a1bb52947f089bea9b8/MAINMENUSS.png)

![image alt](https://github.com/salwatugas/BloomBack-Ari-s-Journey/blob/95118b63acf2b5fb5c60fdfb6c726a593cb465ac/GAMEPLAY1.png)

![image alt](https://github.com/salwatugas/BloomBack-Ari-s-Journey/blob/c5e74aea2ecc6b86037747cd611a7a3f360837d4/GAMEPLAY2.png)

![image alt](https://github.com/salwatugas/BloomBack-Ari-s-Journey/blob/578a6470ed3312fafded5e1adbeeedf2b852cac2/GAMEPLAY3.png)

![image alt](https://github.com/salwatugas/BloomBack-Ari-s-Journey/blob/0e5ce2cd2e59c477079940f6e18015e64b66532a/GAMEPLAY4.png)
