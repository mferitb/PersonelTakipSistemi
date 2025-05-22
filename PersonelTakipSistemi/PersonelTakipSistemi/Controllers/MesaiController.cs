using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.Models.ViewModels;
using PersonelTakipSistemi.Services;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize]
    public class MesaiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MesaiController> _logger;
        private readonly MesaiHesaplamaService _mesaiHesaplamaService;

        public MesaiController(
            ApplicationDbContext context, 
            ILogger<MesaiController> logger,
            MesaiHesaplamaService mesaiHesaplamaService)
        {
            _context = context;
            _logger = logger;
            _mesaiHesaplamaService = mesaiHesaplamaService;
        }

        // GET: Mesai
        public async Task<IActionResult> Index()
        {
            var mesailer = await _context.Mesailer
                .Include(m => m.Personel)
                .Where(m => m.Personel.AktifMi)
                .OrderByDescending(m => m.GirisSaati)
                .Select(m => new MesaiListViewModel
                {
                    Id = m.Id,
                    PersonelAdSoyad = $"{m.Personel.Ad} {m.Personel.Soyad}",
                    GirisSaati = m.GirisSaati,
                    CikisSaati = m.CikisSaati,
                    ToplamSure = m.ToplamSure,
                    Tip = m.Tip,
                    Onaylandi = m.Onaylandi,
                    OnayTarihi = m.OnayTarihi,
                    OnaylayanKullanici = m.OnaylayanKullanici,
                    PersonelMaas = m.Personel.Maas
                })
                .ToListAsync();

            // Mesai ücretlerini hesapla
            foreach (var mesai in mesailer.Where(m => m.Onaylandi && m.CikisSaati.HasValue && m.ToplamSure.HasValue))
            {
                mesai.MesaiUcreti = _mesaiHesaplamaService.MesaiUcretiHesapla(
                    mesai.PersonelMaas,
                    mesai.Tip,
                    mesai.ToplamSure.Value
                );
            }

            return View(mesailer);
        }

        // GET: Mesai/AylikOzet
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AylikOzet(int? personelId = null, int? yil = null, int? ay = null)
        {
            try
            {
                // Varsayılan olarak bu ay
                var hedefTarih = new DateTime(yil ?? DateTime.Now.Year, ay ?? DateTime.Now.Month, 1);
                var baslangicTarih = hedefTarih;
                var bitisTarih = hedefTarih.AddMonths(1).AddDays(-1);

                // Personel listesini hazırla
                var personeller = await _context.Personeller
                    .Where(p => p.AktifMi)
                    .Select(p => new
                    {
                        p.Id,
                        AdSoyad = $"{p.Ad} {p.Soyad}",
                        p.Maas
                    })
                    .ToListAsync();

                ViewBag.Personeller = new SelectList(personeller, "Id", "AdSoyad", personelId);
                ViewBag.Yil = yil ?? DateTime.Now.Year;
                ViewBag.Ay = ay ?? DateTime.Now.Month;

                // Mesai kayıtlarını getir
                var query = _context.Mesailer
                    .Include(m => m.Personel)
                    .Where(m => m.Personel.AktifMi && 
                               m.Onaylandi && 
                               m.CikisSaati.HasValue &&
                               m.GirisSaati >= baslangicTarih && 
                               m.GirisSaati <= bitisTarih);

                if (personelId.HasValue)
                {
                    query = query.Where(m => m.PersonelId == personelId.Value);
                }

                var mesailer = await query.ToListAsync();

                // Özet bilgilerini hazırla
                var ozet = mesailer
                    .GroupBy(m => m.Personel)
                    .Select(g => new MesaiAylikOzetViewModel
                    {
                        PersonelId = g.Key.Id,
                        PersonelAdSoyad = $"{g.Key.Ad} {g.Key.Soyad}",
                        AylikMaas = g.Key.Maas,
                        ToplamMesaiSaati = TimeSpan.FromHours(
                            g.Where(m => m.ToplamSure.HasValue)
                             .Sum(m => m.ToplamSure.Value.TotalHours)
                        ),
                        MesaiUcreti = _mesaiHesaplamaService.AylikMesaiUcretiHesapla(
                            g.Key.Maas,
                            g.Where(m => m.Onaylandi && m.CikisSaati.HasValue)
                        ),
                        MesaiDetaylari = g.Where(m => m.ToplamSure.HasValue)
                            .Select(m => new MesaiDetayViewModel
                            {
                                Tarih = m.GirisSaati.Date,
                                Tip = m.Tip,
                                Sure = m.ToplamSure.Value,
                                Ucret = _mesaiHesaplamaService.MesaiUcretiHesapla(
                                    g.Key.Maas,
                                    m.Tip,
                                    m.ToplamSure.Value
                                )
                            })
                            .OrderByDescending(d => d.Tarih)
                            .ToList()
                    })
                    .ToList();

                return View(ozet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Aylık mesai özeti oluşturulurken hata oluştu");
                TempData["ErrorMessage"] = "Aylık mesai özeti oluşturulurken bir hata oluştu. Lütfen daha sonra tekrar deneyin.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Mesai/Create
        public async Task<IActionResult> Create()
        {
            var personeller = await _context.Personeller
                .Where(p => p.AktifMi)
                .Select(p => new
                {
                    Id = p.Id,
                    AdSoyad = $"{p.Ad} {p.Soyad}"
                })
                .ToListAsync();

            var viewModel = new MesaiCreateViewModel
            {
                Personeller = new SelectList(personeller, "Id", "AdSoyad"),
                GirisSaati = DateTime.Now
            };

            return View(viewModel);
        }

        // POST: Mesai/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MesaiCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var personel = await _context.Personeller.FindAsync(model.PersonelId);
                    if (personel == null)
                    {
                        ModelState.AddModelError("PersonelId", "Seçilen personel bulunamadı.");
                        model.Personeller = new SelectList(await _context.Personeller
                            .Where(p => p.AktifMi)
                            .Select(p => new { Id = p.Id, AdSoyad = $"{p.Ad} {p.Soyad}" })
                            .ToListAsync(), "Id", "AdSoyad");
                        return View(model);
                    }

                    var mesai = new Mesai
                    {
                        PersonelId = model.PersonelId,
                        GirisSaati = model.GirisSaati,
                        PlanlananSure = model.PlanlananSure,
                        Tip = model.Tip,
                        Aciklama = model.Aciklama
                    };

                    // Tahmini ücreti hesapla
                    mesai.TahminiUcret = _mesaiHesaplamaService.MesaiUcretiHesapla(
                        personel.Maas,
                        model.Tip,
                        TimeSpan.FromHours(model.PlanlananSure)
                    );

                    _context.Mesailer.Add(mesai);
                    await _context.SaveChangesAsync();
                    
                    TempData["SuccessMessage"] = "Mesai kaydı başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Mesai kaydı oluşturulurken hata oluştu");
                    ModelState.AddModelError("", "Mesai kaydı oluşturulurken bir hata oluştu. Lütfen tekrar deneyin.");
                }
            }

            model.Personeller = new SelectList(await _context.Personeller
                .Where(p => p.AktifMi)
                .Select(p => new { Id = p.Id, AdSoyad = $"{p.Ad} {p.Soyad}" })
                .ToListAsync(), "Id", "AdSoyad");
            return View(model);
        }

        // POST: Mesai/Cikis/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cikis(int id)
        {
            var mesai = await _context.Mesailer.FindAsync(id);
            if (mesai == null)
            {
                return NotFound();
            }

            if (mesai.CikisSaati.HasValue)
            {
                TempData["ErrorMessage"] = "Bu mesai kaydı için zaten çıkış yapılmış.";
                return RedirectToAction(nameof(Index));
            }

            mesai.CikisSaati = DateTime.Now;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Çıkış kaydı başarıyla oluşturuldu.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Mesai/Onayla/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Onayla(int id)
        {
            var mesai = await _context.Mesailer.FindAsync(id);
            if (mesai == null)
            {
                return NotFound();
            }

            mesai.Onaylandi = true;
            mesai.OnayTarihi = DateTime.Now;
            mesai.OnaylayanKullanici = User.Identity.Name;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Mesai kaydı onaylandı.";
            return RedirectToAction(nameof(Index));
        }
    }
} 