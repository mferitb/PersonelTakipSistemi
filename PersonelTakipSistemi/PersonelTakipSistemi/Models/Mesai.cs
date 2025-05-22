using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonelTakipSistemi.Models
{
    public class Mesai
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PersonelId { get; set; }
        public Personel Personel { get; set; }

        [Required]
        [Display(Name = "Giriş Saati")]
        [DataType(DataType.DateTime)]
        public DateTime GirisSaati { get; set; }

        [Display(Name = "Çıkış Saati")]
        [DataType(DataType.DateTime)]
        public DateTime? CikisSaati { get; set; }

        [Display(Name = "Planlanan Süre (Saat)")]
        public int PlanlananSure { get; set; }

        [Display(Name = "Toplam Süre")]
        public TimeSpan? ToplamSure => CikisSaati.HasValue ? CikisSaati.Value - GirisSaati : null;

        [Required]
        [Display(Name = "Mesai Tipi")]
        public MesaiTipi Tip { get; set; } = MesaiTipi.Normal;

        [Display(Name = "Açıklama")]
        [StringLength(500)]
        public string? Aciklama { get; set; }

        [Display(Name = "Onaylandı")]
        public bool Onaylandi { get; set; } = false;
        [Display(Name = "Onay Tarihi")]
        public DateTime? OnayTarihi { get; set; }
        [Display(Name = "Onaylayan")]
        public string? OnaylayanKullanici { get; set; }

        // Tahmini ücret hesaplama (planlanan süreye göre)
        [NotMapped]
        public decimal TahminiUcret { get; set; }
    }

    public enum MesaiTipi
    {
        Normal,
        FazlaMesai,
        TatilMesai,
        GeceMesai
    }
} 