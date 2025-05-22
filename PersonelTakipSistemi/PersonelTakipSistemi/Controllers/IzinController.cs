using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.Models.ViewModels;
using Microsoft.Extensions.Logging;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize]
    public class IzinController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IzinController> _logger;

        public IzinController(ApplicationDbContext context, ILogger<IzinController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Izin
        public async Task<IActionResult> Index()
        {
            var izinler = await _context.Izinler
                .Include(i => i.Personel)
                .Include(i => i.Onaylayan)
                .Where(i => i.Personel.AktifMi)
                .Select(i => new IzinListViewModel
                {
                    Id = i.Id,
                    PersonelAdSoyad = $"{i.Personel.Ad} {i.Personel.Soyad}",
                    IzinTuru = i.IzinTuru,
                    BaslangicTarihi = i.BaslangicTarihi,
                    BitisTarihi = i.BitisTarihi,
                    OnayDurumu = i.OnayDurumu,
                    OnaylayanAdSoyad = i.Onaylayan != null ? i.Onaylayan.FullName : "-",
                    OnayTarihi = i.OnayTarihi
                })
                .OrderByDescending(i => i.BaslangicTarihi)
                .ToListAsync();

            return View(izinler);
        }

        // GET: Izin/Create
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

            var viewModel = new IzinCreateViewModel
            {
                Personeller = new SelectList(personeller, "Id", "AdSoyad")
            };

            return View(viewModel);
        }

        // POST: Izin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IzinCreateViewModel model)
        {
            _logger.LogInformation("İzin oluşturma isteği alındı. Model: {@Model}", model);

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Model geçerli, izin oluşturuluyor...");
                
                if (model.BitisTarihi < model.BaslangicTarihi)
                {
                    _logger.LogWarning("Bitiş tarihi başlangıç tarihinden önce: {BitisTarihi} < {BaslangicTarihi}", 
                        model.BitisTarihi, model.BaslangicTarihi);
                    ModelState.AddModelError("BitisTarihi", "Bitiş tarihi başlangıç tarihinden önce olamaz.");
                    model.Personeller = new SelectList(await _context.Personeller
                        .Where(p => p.AktifMi)
                        .Select(p => new { Id = p.Id, AdSoyad = $"{p.Ad} {p.Soyad}" })
                        .ToListAsync(), "Id", "AdSoyad");
                    return View(model);
                }

                try
                {
                    var izin = new Izin
                    {
                        PersonelId = model.PersonelId,
                        BaslangicTarihi = model.BaslangicTarihi,
                        BitisTarihi = model.BitisTarihi,
                        IzinTuru = model.IzinTuru,
                        Aciklama = model.Aciklama,
                        OnayDurumu = IzinOnayDurumu.Beklemede
                    };

                    _context.Izinler.Add(izin);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("İzin başarıyla oluşturuldu. İzin ID: {IzinId}", izin.Id);
                    
                    TempData["SuccessMessage"] = "İzin talebi başarıyla oluşturuldu.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "İzin oluşturulurken hata oluştu");
                    ModelState.AddModelError("", "İzin oluşturulurken bir hata oluştu. Lütfen tekrar deneyin.");
                }
            }
            else
            {
                _logger.LogWarning("Model geçersiz. Hatalar: {@ModelStateErrors}", 
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            }

            model.Personeller = new SelectList(await _context.Personeller
                .Where(p => p.AktifMi)
                .Select(p => new { Id = p.Id, AdSoyad = $"{p.Ad} {p.Soyad}" })
                .ToListAsync(), "Id", "AdSoyad");
            return View(model);
        }

        // POST: Izin/Onayla/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Onayla(int id, bool onayla)
        {
            var izin = await _context.Izinler.FindAsync(id);
            if (izin == null)
            {
                return NotFound();
            }

            // Admin kullanıcısını bul
            var adminUsername = User.Identity.Name;
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == adminUsername);
            if (admin == null)
            {
                return NotFound("Admin kullanıcısı bulunamadı.");
            }

            izin.OnayDurumu = onayla ? IzinOnayDurumu.Onaylandi : IzinOnayDurumu.Reddedildi;
            izin.OnayTarihi = DateTime.Now;
            izin.OnaylayanId = admin.Id;

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = onayla ? "İzin talebi onaylandı." : "İzin talebi reddedildi.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Izin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var izin = await _context.Izinler.FindAsync(id);
            if (izin == null)
            {
                return NotFound();
            }

            // Sadece beklemedeki izinler silinebilir
            if (izin.OnayDurumu != IzinOnayDurumu.Beklemede)
            {
                TempData["ErrorMessage"] = "Sadece beklemedeki izin talepleri silinebilir.";
                return RedirectToAction(nameof(Index));
            }

            _context.Izinler.Remove(izin);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "İzin talebi başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
} 