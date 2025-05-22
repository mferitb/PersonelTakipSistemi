using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonelTakipSistemi.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int ToplamPersonel { get; set; }
        public int AktifIzinler { get; set; }
        public int BugunGirisYapanlar { get; set; }
        public List<PersonelListViewModel> SonEklenenPersoneller { get; set; }
    }
} 