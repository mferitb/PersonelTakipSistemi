using PersonelTakipSistemi.Models;

namespace PersonelTakipSistemi.Services
{
    public class MesaiHesaplamaService
    {
        // Mesai ücret katsayıları
        private const double NormalMesaiKatsayisi = 1.5;    // Normal mesai %50 fazla
        private const double FazlaMesaiKatsayisi = 2.0;     // Fazla mesai %100 fazla
        private const double TatilMesaiKatsayisi = 2.5;     // Tatil mesai %150 fazla
        private const double GeceMesaiKatsayisi = 2.0;      // Gece mesai %100 fazla

        // Saatlik ücret hesaplama (aylık maaş / aylık çalışma saati)
        private const double AylikCalismaSaati = 225.0; // Ayda ortalama 225 saat çalışma

        public decimal MesaiUcretiHesapla(decimal aylikMaas, MesaiTipi mesaiTipi, TimeSpan mesaiSuresi)
        {
            // Saatlik ücret hesaplama
            decimal saatlikUcret = aylikMaas / (decimal)AylikCalismaSaati;

            // Mesai katsayısını belirleme
            double katsayi = mesaiTipi switch
            {
                MesaiTipi.Normal => NormalMesaiKatsayisi,
                MesaiTipi.FazlaMesai => FazlaMesaiKatsayisi,
                MesaiTipi.TatilMesai => TatilMesaiKatsayisi,
                MesaiTipi.GeceMesai => GeceMesaiKatsayisi,
                _ => 1.0
            };

            // Mesai süresini saat cinsinden hesaplama
            double mesaiSaati = mesaiSuresi.TotalHours;

            // Mesai ücretini hesaplama
            decimal mesaiUcreti = saatlikUcret * (decimal)katsayi * (decimal)mesaiSaati;

            // Kuruş hassasiyetini ayarlama
            return Math.Round(mesaiUcreti, 2);
        }

        public decimal AylikMesaiUcretiHesapla(decimal aylikMaas, IEnumerable<Mesai> mesailer)
        {
            decimal toplamMesaiUcreti = 0;

            foreach (var mesai in mesailer.Where(m => m.Onaylandi && m.CikisSaati.HasValue))
            {
                var mesaiSuresi = mesai.ToplamSure ?? TimeSpan.Zero;
                toplamMesaiUcreti += MesaiUcretiHesapla(aylikMaas, mesai.Tip, mesaiSuresi);
            }

            return toplamMesaiUcreti;
        }
    }
} 