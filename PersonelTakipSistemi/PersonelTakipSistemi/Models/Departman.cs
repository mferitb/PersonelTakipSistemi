using System.ComponentModel.DataAnnotations;

namespace PersonelTakipSistemi.Models
{
    public class Departman
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Departman adı zorunludur.")]
        [StringLength(100)]
        public string Ad { get; set; }

        public string? Aciklama { get; set; }

        // Navigation property
        public ICollection<Personel> Personeller { get; set; }
    }
} 