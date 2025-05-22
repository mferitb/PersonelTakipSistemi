using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Debug için izin kayıtlarını kontrol et (Tümü)
            var bugun = DateTime.Today;
            var tumIzinler = await _context.Izinler
                .Include(i => i.Personel)
                .Select(i => new { 
                    Personel = $"{i.Personel.Ad} {i.Personel.Soyad}",
                    Baslangic = i.BaslangicTarihi,
                    Bitis = i.BitisTarihi,
                    OnayDurumu = i.OnayDurumu
                })
                .ToListAsync();

            _logger.LogInformation("Tüm izin kayıtları: {@TumIzinler}", tumIzinler);

            // Debug için bugün için aktif izinleri kontrol et
            var aktifIzinlerListesi = tumIzinler.Where(i => i.Baslangic <= bugun && i.Bitis >= bugun).ToList();
            _logger.LogInformation("Bugün için aktif izinler (filtrelenmiş): {@AktifIzinlerListesi}", aktifIzinlerListesi);

            var dashboardViewModel = new DashboardViewModel
            {
                ToplamPersonel = await _context.Personeller.CountAsync(p => p.AktifMi),
                AktifIzinler = aktifIzinlerListesi.Count,
                BugunGirisYapanlar = await _context.Mesailer
                    .CountAsync(m => m.GirisSaati.Date == DateTime.Today),
                SonEklenenPersoneller = await _context.Personeller
                    .OrderByDescending(p => p.IseGirisTarihi)
                    .Take(5)
                    .Select(p => new PersonelListViewModel
                    {
                        Id = p.Id,
                        AdSoyad = $"{p.Ad} {p.Soyad}",
                        Departman = p.Departman.Ad,
                        Pozisyon = p.Pozisyon.Ad,
                        IseGirisTarihi = p.IseGirisTarihi
                    })
                    .ToListAsync()
            };

            return View(dashboardViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Models.ViewModels.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
