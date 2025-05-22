using System.ComponentModel.DataAnnotations;
using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Models.ViewModels
{
    public class MesaiListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Personel")]
        public string PersonelAdSoyad { get; set; }

        [Display(Name = "Giriş Saati")]
        [DataType(DataType.DateTime)]
        public DateTime GirisSaati { get; set; }

        [Display(Name = "Çıkış Saati")]
        [DataType(DataType.DateTime)]
        public DateTime? CikisSaati { get; set; }

        [Display(Name = "Toplam Süre")]
        public TimeSpan? ToplamSure { get; set; }

        [Display(Name = "Mesai Tipi")]
        public MesaiTipi Tip { get; set; }

        [Display(Name = "Onaylandı")]
        public bool Onaylandi { get; set; }

        [Display(Name = "Onay Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime? OnayTarihi { get; set; }

        [Display(Name = "Onaylayan")]
        public string OnaylayanKullanici { get; set; }

        [Display(Name = "Mesai Ücreti")]
        [DataType(DataType.Currency)]
        public decimal? MesaiUcreti { get; set; }

        // Personel maaş bilgisi için
        public decimal PersonelMaas { get; set; }
    }
} 