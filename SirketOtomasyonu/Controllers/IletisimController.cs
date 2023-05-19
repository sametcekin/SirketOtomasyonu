using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SirketOtomasyonu.Data;
using SirketOtomasyonu.Data.Entities;
using SirketOtomasyonu.Models.Iletisim;

namespace SirketOtomasyonu.Controllers
{
    [Authorize]
    public class IletisimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IletisimController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var iletisimListe = _context.Iletisim.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                iletisimListe = iletisimListe.Where(x => x.AdiSoyadi.Contains(searchString)
                || x.Email.Contains(searchString) || x.Baslik.Contains(searchString));
            }
            var model = new List<IletisimViewModel>();
            foreach (var item in await iletisimListe.ToListAsync())
            {
                model.Add(new IletisimViewModel
                {
                    Id = item.Id,
                    AdiSoyadi = item.AdiSoyadi,
                    Baslik = item.Baslik,
                    Email = item.Email,
                    Mesaj = item.Mesaj,
                    Tarih = item.Tarih,
                });
            }
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdiSoyadi,Email,Baslik,Mesaj")] Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                iletisim.Tarih = DateTime.Now;
                _context.Add(iletisim);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(iletisim);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Iletisim == null)
            {
                return NotFound();
            }

            var iletisim = await _context.Iletisim
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iletisim == null)
            {
                return NotFound();
            }
            var model = new IletisimViewModel
            {
                Id = iletisim.Id,
                AdiSoyadi = iletisim.AdiSoyadi,
                Baslik = iletisim.Baslik,
                Email = iletisim.Email,
                Mesaj = iletisim.Mesaj,
                Tarih = iletisim.Tarih,
            };
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Iletisim == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Iletisim'  is null.");
            }
            var iletisim = await _context.Iletisim.FindAsync(id);
            if (iletisim != null)
            {
                _context.Iletisim.Remove(iletisim);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IletisimExists(int id)
        {
            return _context.Iletisim.Any(e => e.Id == id);
        }
    }
}
