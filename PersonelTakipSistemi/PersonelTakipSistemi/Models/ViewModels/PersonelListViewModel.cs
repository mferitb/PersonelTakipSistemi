using System.ComponentModel.DataAnnotations;

namespace PersonelTakipSistemi.Models.ViewModels
{
    public class PersonelListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; }

        [Display(Name = "Departman")]
        public string Departman { get; set; }

        [Display(Name = "Pozisyon")]
        public string Pozisyon { get; set; }

        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        public string Telefon { get; set; }

        [Display(Name = "İşe Giriş Tarihi")]
        [DataType(DataType.Date)]
        public DateTime IseGirisTarihi { get; set; }
    }
} 