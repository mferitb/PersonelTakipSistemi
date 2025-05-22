using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonelTakipSistemi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PersonelTakipSistemi.Models.ViewModels
{
    public class IzinCreateViewModel
    {
        [Required(ErrorMessage = "Personel seçimi zorunludur")]
        [Display(Name = "Personel")]
        public int PersonelId { get; set; }

        [Required(ErrorMessage = "İzin başlangıç tarihi zorunludur")]
        [Display(Name = "İzin Başlangıç Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BaslangicTarihi { get; set; }

        [Required(ErrorMessage = "İzin bitiş tarihi zorunludur")]
        [Display(Name = "İzin Bitiş Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BitisTarihi { get; set; }

        [Required(ErrorMessage = "İzin türü seçimi zorunludur")]
        [Display(Name = "İzin Türü")]
        public IzinTuru IzinTuru { get; set; }

        [Display(Name = "Açıklama")]
        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string Aciklama { get; set; }

        // Personel seçimi için dropdown listesi
        [ValidateNever]
        public SelectList Personeller { get; set; }
    }
} 