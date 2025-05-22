using System.ComponentModel.DataAnnotations;

namespace PersonelTakipSistemi.Models
{
    public class Personel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [StringLength(50)]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [StringLength(50)]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "TC Kimlik No zorunludur.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır.")]
        public string TCKimlikNo { get; set; }

        [Required(ErrorMessage = "Doğum tarihi zorunludur.")]
        [DataType(DataType.Date)]
        public DateTime DogumTarihi { get; set; }

        [Required(ErrorMessage = "İşe giriş tarihi zorunludur.")]
        [DataType(DataType.Date)]
        public DateTime IseGirisTarihi { get; set; }

        [DataType(DataType.Date)]
        public DateTime? IstenCikisTarihi { get; set; }

        [Required(ErrorMessage = "Departman seçimi zorunludur.")]
        public int DepartmanId { get; set; }
        public Departman Departman { get; set; }

        [Required(ErrorMessage = "Pozisyon seçimi zorunludur.")]
        public int PozisyonId { get; set; }
        public Pozisyon Pozisyon { get; set; }

        [Required(ErrorMessage = "Maaş bilgisi zorunludur.")]
        [Range(0, double.MaxValue, ErrorMessage = "Maaş 0'dan büyük olmalıdır.")]
        public decimal Maas { get; set; }

        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string? Telefon { get; set; }

        public string? Adres { get; set; }

        public bool AktifMi { get; set; } = true;

        // Navigation properties
        public ICollection<Izin> Izinler { get; set; }
        public ICollection<Mesai> Mesailer { get; set; }
    }
} 