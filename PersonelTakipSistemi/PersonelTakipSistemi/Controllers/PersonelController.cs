using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelTakipSistemi.Data;
using PersonelTakipSistemi.Models;
using PersonelTakipSistemi.Models.ViewModels;

namespace PersonelTakipSistemi.Controllers
{
    [Authorize] // Tüm controller'ı yetkilendirme gerektirir hale getir
    public class PersonelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personel
        public async Task<IActionResult> Index()
        {
            var personeller = await _context.Personeller
                .Include(p => p.Departman)
                .Include(p => p.Pozisyon)
                .Where(p => p.AktifMi)
                .Select(p => new PersonelListViewModel
                {
                    Id = p.Id,
                    AdSoyad = $"{p.Ad} {p.Soyad}",
                    Departman = p.Departman.Ad,
                    Pozisyon = p.Pozisyon.Ad,
                    Email = p.Email,
                    Telefon = p.Telefon,
                    IseGirisTarihi = p.IseGirisTarihi
                })
                .ToListAsync();

            return View(personeller);
        }

        // GET: Personel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonelCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Departmanı bul veya oluştur
                var departman = await _context.Departmanlar
                    .FirstOrDefaultAsync(d => d.Ad.ToLower() == model.DepartmanAd.ToLower());

                if (departman == null)
                {
                    departman = new Departman { Ad = model.DepartmanAd };
                    _context.Departmanlar.Add(departman);
                    await _context.SaveChangesAsync();
                }

                // Pozisyonu bul veya oluştur
                var pozisyon = await _context.Pozisyonlar
                    .FirstOrDefaultAsync(p => p.Ad.ToLower() == model.PozisyonAd.ToLower());

                if (pozisyon == null)
                {
                    pozisyon = new Pozisyon { Ad = model.PozisyonAd };
                    _context.Pozisyonlar.Add(pozisyon);
                    await _context.SaveChangesAsync();
                }

                var personel = new Personel
                {
                    TCKimlikNo = model.TCKimlikNo,
                    Ad = model.Ad,
                    Soyad = model.Soyad,
                    Email = model.Email,
                    Telefon = model.Telefon,
                    Adres = model.Adres,
                    DepartmanId = departman.Id,
                    PozisyonId = pozisyon.Id,
                    IseGirisTarihi = model.IseGirisTarihi,
                    Maas = model.Maas,
                    AktifMi = true
                };

                _context.Personeller.Add(personel);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Personel başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Personel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personel = await _context.Personeller
                .Include(p => p.Departman)
                .Include(p => p.Pozisyon)
                .FirstOrDefaultAsync(p => p.Id == id && p.AktifMi);

            if (personel == null)
            {
                return NotFound();
            }

            var viewModel = new PersonelDetailsViewModel
            {
                Id = personel.Id,
                TCKimlikNo = personel.TCKimlikNo,
                Ad = personel.Ad,
                Soyad = personel.Soyad,
                Email = personel.Email,
                Telefon = personel.Telefon,
                Adres = personel.Adres,
                DepartmanAd = personel.Departman.Ad,
                PozisyonAd = personel.Pozisyon.Ad,
                IseGirisTarihi = personel.IseGirisTarihi,
                Maas = personel.Maas
            };

            return View(viewModel);
        }

        // GET: Personel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personel = await _context.Personeller
                .Include(p => p.Departman)
                .Include(p => p.Pozisyon)
                .FirstOrDefaultAsync(p => p.Id == id && p.AktifMi);

            if (personel == null)
            {
                return NotFound();
            }

            var viewModel = new PersonelEditViewModel
            {
                Id = personel.Id,
                TCKimlikNo = personel.TCKimlikNo,
                Ad = personel.Ad,
                Soyad = personel.Soyad,
                Email = personel.Email,
                Telefon = personel.Telefon,
                Adres = personel.Adres,
                DepartmanAd = personel.Departman.Ad,
                PozisyonAd = personel.Pozisyon.Ad,
                IseGirisTarihi = personel.IseGirisTarihi,
                Maas = personel.Maas
            };

            return View(viewModel);
        }

        // POST: Personel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PersonelEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var personel = await _context.Personeller.FindAsync(id);
                    if (personel == null || !personel.AktifMi)
                    {
                        return NotFound();
                    }

                    // Departmanı bul veya oluştur
                    var departman = await _context.Departmanlar
                        .FirstOrDefaultAsync(d => d.Ad.ToLower() == model.DepartmanAd.ToLower());

                    if (departman == null)
                    {
                        departman = new Departman { Ad = model.DepartmanAd };
                        _context.Departmanlar.Add(departman);
                        await _context.SaveChangesAsync();
                    }

                    // Pozisyonu bul veya oluştur
                    var pozisyon = await _context.Pozisyonlar
                        .FirstOrDefaultAsync(p => p.Ad.ToLower() == model.PozisyonAd.ToLower());

                    if (pozisyon == null)
                    {
                        pozisyon = new Pozisyon { Ad = model.PozisyonAd };
                        _context.Pozisyonlar.Add(pozisyon);
                        await _context.SaveChangesAsync();
                    }

                    // Personel bilgilerini güncelle
                    personel.TCKimlikNo = model.TCKimlikNo;
                    personel.Ad = model.Ad;
                    personel.Soyad = model.Soyad;
                    personel.Email = model.Email;
                    personel.Telefon = model.Telefon;
                    personel.Adres = model.Adres;
                    personel.DepartmanId = departman.Id;
                    personel.PozisyonId = pozisyon.Id;
                    personel.IseGirisTarihi = model.IseGirisTarihi;
                    personel.Maas = model.Maas;

                    _context.Update(personel);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Personel bilgileri başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonelExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(model);
        }

        // POST: Personel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personel = await _context.Personeller.FindAsync(id);
            if (personel != null)
            {
                personel.AktifMi = false; // Soft delete
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Personel başarıyla silindi.";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Personel/GetPersonelMaaslar
        [HttpGet]
        public async Task<IActionResult> GetPersonelMaaslar()
        {
            var maaslar = await _context.Personeller
                .Where(p => p.AktifMi)
                .Select(p => new { p.Id, p.Maas })
                .ToDictionaryAsync(p => p.Id, p => p.Maas);

            return Json(maaslar);
        }

        private bool PersonelExists(int id)
        {
            return _context.Personeller.Any(e => e.Id == id && e.AktifMi);
        }
    }
} 