using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonelTakipSistemi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PersonelTakipSistemi.Models.ViewModels
{
    public class MesaiCreateViewModel
    {
        [Required(ErrorMessage = "Personel seçimi zorunludur")]
        [Display(Name = "Personel")]
        public int PersonelId { get; set; }

        [Required(ErrorMessage = "Giriş saati zorunludur")]
        [Display(Name = "Giriş Saati")]
        [DataType(DataType.DateTime)]
        public DateTime GirisSaati { get; set; }

        [Required(ErrorMessage = "Planlanan süre zorunludur")]
        [Display(Name = "Planlanan Süre (Saat)")]
        [Range(1, 24, ErrorMessage = "Planlanan süre 1-24 saat arasında olmalıdır")]
        public int PlanlananSure { get; set; }

        [Required(ErrorMessage = "Mesai tipi zorunludur")]
        [Display(Name = "Mesai Tipi")]
        public MesaiTipi Tip { get; set; } = MesaiTipi.Normal;

        [Display(Name = "Açıklama")]
        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string Aciklama { get; set; }

        [ValidateNever]
        public SelectList Personeller { get; set; }
    }
} 