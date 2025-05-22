using System.ComponentModel.DataAnnotations;

namespace PersonelTakipSistemi.Models.ViewModels
{
    public class PersonelDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "TC Kimlik No")]
        public string TCKimlikNo { get; set; }

        [Display(Name = "Ad")]
        public string Ad { get; set; }

        [Display(Name = "Soyad")]
        public string Soyad { get; set; }

        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        public string Telefon { get; set; }

        [Display(Name = "Adres")]
        public string Adres { get; set; }

        [Display(Name = "Departman")]
        public string DepartmanAd { get; set; }

        [Display(Name = "Pozisyon")]
        public string PozisyonAd { get; set; }

        [Display(Name = "İşe Giriş Tarihi")]
        [DataType(DataType.Date)]
        public DateTime IseGirisTarihi { get; set; }

        [Display(Name = "Maaş")]
        [DataType(DataType.Currency)]
        public decimal Maas { get; set; }
    }
} 