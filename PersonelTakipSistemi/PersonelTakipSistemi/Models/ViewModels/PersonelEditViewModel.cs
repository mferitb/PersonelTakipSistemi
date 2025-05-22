using System.ComponentModel.DataAnnotations;

namespace PersonelTakipSistemi.Models.ViewModels
{
    public class PersonelEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "TC Kimlik No alanı zorunludur")]
        [Display(Name = "TC Kimlik No")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "TC Kimlik No sadece rakamlardan oluşmalıdır")]
        public string TCKimlikNo { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur")]
        [Display(Name = "Ad")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur")]
        [Display(Name = "Soyad")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "E-posta alanı zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon alanı zorunludur")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
        [Display(Name = "Telefon")]
        public string Telefon { get; set; }

        [Display(Name = "Adres")]
        [StringLength(200, ErrorMessage = "Adres en fazla 200 karakter olabilir")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Departman alanı zorunludur")]
        [Display(Name = "Departman")]
        [StringLength(50, ErrorMessage = "Departman adı en fazla 50 karakter olabilir")]
        public string DepartmanAd { get; set; }

        [Required(ErrorMessage = "Pozisyon alanı zorunludur")]
        [Display(Name = "Pozisyon")]
        [StringLength(50, ErrorMessage = "Pozisyon adı en fazla 50 karakter olabilir")]
        public string PozisyonAd { get; set; }

        [Required(ErrorMessage = "İşe giriş tarihi zorunludur")]
        [Display(Name = "İşe Giriş Tarihi")]
        [DataType(DataType.Date)]
        public DateTime IseGirisTarihi { get; set; }

        [Required(ErrorMessage = "Maaş alanı zorunludur")]
        [Display(Name = "Maaş")]
        [Range(0, double.MaxValue, ErrorMessage = "Maaş 0'dan büyük olmalıdır")]
        public decimal Maas { get; set; }
    }
} 