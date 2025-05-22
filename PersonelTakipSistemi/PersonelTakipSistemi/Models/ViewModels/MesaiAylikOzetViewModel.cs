using System.ComponentModel.DataAnnotations;

namespace PersonelTakipSistemi.Models.ViewModels
{
    public class MesaiAylikOzetViewModel
    {
        public int PersonelId { get; set; }

        [Display(Name = "Personel")]
        public string PersonelAdSoyad { get; set; }

        [Display(Name = "Aylık Maaş")]
        [DataType(DataType.Currency)]
        public decimal AylikMaas { get; set; }

        [Display(Name = "Toplam Mesai Saati")]
        public TimeSpan ToplamMesaiSaati { get; set; }

        [Display(Name = "Toplam Mesai Ücreti")]
        [DataType(DataType.Currency)]
        public decimal MesaiUcreti { get; set; }

        public List<MesaiDetayViewModel> MesaiDetaylari { get; set; }
    }

    public class MesaiDetayViewModel
    {
        [Display(Name = "Tarih")]
        [DataType(DataType.Date)]
        public DateTime Tarih { get; set; }

        [Display(Name = "Mesai Tipi")]
        public MesaiTipi Tip { get; set; }

        [Display(Name = "Süre")]
        public TimeSpan Sure { get; set; }

        [Display(Name = "Ücret")]
        [DataType(DataType.Currency)]
        public decimal Ucret { get; set; }
    }
} 