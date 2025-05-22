# Personel Takip Sistemi

## Proje Açıklaması

Bu proje, bir kuruluşun personel bilgilerini, izin taleplerini ve mesai (çalışma saatleri/fazla mesai) kayıtlarını takip etmek için geliştirilmiş bir web uygulamasıdır.

## Özellikler

- Personel ekleme, düzenleme ve listeleme.
- İzin taleplerini yönetme (oluşturma, onaylama, reddetme).
- Mesai (çalışma saati/fazla mesai) kayıtlarını tutma.
- Ana sayfada (Dashboard) özet bilgileri görüntüleme (Toplam Personel, Aktif İzinler, Bugün Giriş Yapanlar vb.).
- Yönetici girişi ve yetkilendirme.
- Fazla mesai türlerine göre maaş hesaplama altyapısı.

## Teknolojiler

- ASP.NET Core MVC
- Entity Framework Core (Veritabanı işlemleri için)
- Microsoft SQL Server (Veritabanı)
- Bootstrap (Kullanıcı arayüzü için)
- C#

## Kurulum

Projeyi yerel ortamınızda kurmak ve çalıştırmak için aşağıdaki adımları izleyin:

1.  **Depoyu Klonlayın:**

    ```bash
    git clone <repo_adresiniz>
    cd PersonelTakipSistemi
    ```

2.  **Veritabanı Yapılandırması:**

    - `appsettings.json` dosyasını açın.
    - `ConnectionStrings` bölümündeki `DefaultConnection` değerini kendi SQL Server bağlantı string'inizle güncelleyin.

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PersonelTakipSistemi;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```

3.  **Veritabanı Migrasyonlarını Uygulayın:**

    Proje dizininde terminali açın ve aşağıdaki komutları çalıştırın:

    ```bash
    dotnet ef database update
    ```

    Bu komut, veritabanını oluşturacak ve gerekli tabloları ekleyecektir.

4.  **Uygulamayı Çalıştırın:**

    Proje dizininde terminali açın ve aşağıdaki komutu çalıştırın:

    ```bash
    dotnet run
    ```

    Uygulama başlatıldığında konsolda uygulamanın çalıştığı adres (genellikle `http://localhost:<port>`) görünecektir.

## Kullanım

Uygulama çalıştıktan sonra tarayıcınızdan belirtilen adrese giderek erişebilirsiniz.

Yönetici girişi için varsayılan kimlik bilgileri (eğer seeding yapıldıysa veya manuel eklediyseniz) kullanılabilir. Yeni personel ve izin kayıtları oluşturmak için ilgili menüleri kullanabilirsiniz.

## Katkıda Bulunma

Katkıda bulunmak isterseniz lütfen iletişime geçin veya pull request gönderin.

## Lisans

Bu proje MIT Lisansı ile lisanslanmıştır. Daha fazla bilgi için `LICENSE` dosyasına bakabilirsiniz (Eğer varsa). 

![Ekran görüntüsü 2025-05-22 230259](https://github.com/user-attachments/assets/4462ad14-cb35-47ab-a6ee-65e28decf261)
![Ekran görüntüsü 2025-05-22 230331](https://github.com/user-attachments/assets/e1668e28-a37f-4965-b188-b7bc13db24fa)
![Ekran görüntüsü 2025-05-22 230355](https://github.com/user-attachments/assets/ec8b5013-0325-43be-9191-70bf18c9a3bb)
![Ekran görüntüsü 2025-05-22 230531](https://github.com/user-attachments/assets/ce1e8c2b-695f-42c8-9dab-9d0ca2737c0a)
![Ekran görüntüsü 2025-05-22 230455](https://github.com/user-attachments/assets/b494c308-efb6-4bef-8ea9-2e1769b71dd9)
![Ekran görüntüsü 2025-05-22 230439](https://github.com/user-attachments/assets/997e598b-3544-4fcd-a72a-6a10054fe7fc)
![Ekran görüntüsü 2025-05-22 230513](https://github.com/user-attachments/assets/2cdf6d45-fb03-4a89-b25f-bd4aecf7d79a)
![Ekran görüntüsü 2025-05-22 230637](https://github.com/user-attachments/assets/4de33295-70fb-4e7b-89d2-69c97e48341c)
