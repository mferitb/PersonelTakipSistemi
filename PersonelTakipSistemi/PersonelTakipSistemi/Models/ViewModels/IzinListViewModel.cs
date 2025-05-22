using System.ComponentModel.DataAnnotations;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Models.ViewModels
{
    public class IzinListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Personel")]
        public string PersonelAdSoyad { get; set; }

        [Display(Name = "İzin Türü")]
        public IzinTuru IzinTuru { get; set; }

        [Display(Name = "Başlangıç Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BaslangicTarihi { get; set; }

        [Display(Name = "Bitiş Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BitisTarihi { get; set; }

        [Display(Name = "Onay Durumu")]
        public IzinOnayDurumu OnayDurumu { get; set; }

        [Display(Name = "Onaylayan")]
        public string OnaylayanAdSoyad { get; set; }

        [Display(Name = "Onay Tarihi")]
        [DataType(DataType.Date)]
        public DateTime? OnayTarihi { get; set; }
    }
} 