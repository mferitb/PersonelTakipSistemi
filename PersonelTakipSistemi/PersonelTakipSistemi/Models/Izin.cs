using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonelTakipSistemi.Models
{
    public class Izin
    {
        public int Id { get; set; }

        [Required]
        public int PersonelId { get; set; }

        [Required]
        [Display(Name = "İzin Başlangıç Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BaslangicTarihi { get; set; }

        [Required]
        [Display(Name = "İzin Bitiş Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BitisTarihi { get; set; }

        [Required]
        [Display(Name = "İzin Türü")]
        public IzinTuru IzinTuru { get; set; }

        [Display(Name = "Açıklama")]
        [StringLength(500)]
        public string Aciklama { get; set; }

        [Display(Name = "Onay Durumu")]
        public IzinOnayDurumu OnayDurumu { get; set; } = IzinOnayDurumu.Beklemede;

        [Display(Name = "Onay Tarihi")]
        public DateTime? OnayTarihi { get; set; }

        [Display(Name = "Onaylayan")]
        public int? OnaylayanId { get; set; }

        [ForeignKey("PersonelId")]
        public virtual Personel Personel { get; set; }

        [ForeignKey("OnaylayanId")]
        public virtual Admin Onaylayan { get; set; }
    }

    public enum IzinTuru
    {
        [Display(Name = "Yıllık İzin")]
        YillikIzin,
        [Display(Name = "Hastalık İzni")]
        HastalikIzin,
        [Display(Name = "Mazeret İzni")]
        MazeretIzin,
        [Display(Name = "Ücretsiz İzin")]
        UcretsizIzin
    }

    public enum IzinOnayDurumu
    {
        [Display(Name = "Beklemede")]
        Beklemede,
        [Display(Name = "Onaylandı")]
        Onaylandi,
        [Display(Name = "Reddedildi")]
        Reddedildi
    }
} 