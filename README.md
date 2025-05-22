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
