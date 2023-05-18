using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SirketOtomasyonu.Data;
using SirketOtomasyonu.Data.Entities;

namespace SirketOtomasyonu.Controllers
{
    [Authorize]
    public class DuyuruController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DuyuruController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var duyuruList = _context.Duyuru.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                duyuruList = duyuruList.Where(x => x.Baslik.Contains(searchString)
                || x.Icerik.Contains(searchString));
            }
            return View(await duyuruList.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Baslik,Icerik,Aciklama,Tarih")] Duyuru duyuru)
        {
            if (ModelState.IsValid)
            {
                _context.Add(duyuru);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(duyuru);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Duyuru == null)
            {
                return NotFound();
            }

            var duyuru = await _context.Duyuru.FindAsync(id);
            if (duyuru == null)
            {
                return NotFound();
            }
            return View(duyuru);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Baslik,Icerik,Aciklama,Tarih")] Duyuru duyuru)
        {
            if (id != duyuru.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duyuru);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DuyuruExists(duyuru.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(duyuru);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Duyuru == null)
            {
                return NotFound();
            }

            var duyuru = await _context.Duyuru
                .FirstOrDefaultAsync(m => m.Id == id);
            if (duyuru == null)
            {
                return NotFound();
            }

            return View(duyuru);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Duyuru == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Duyuru'  is null.");
            }
            var duyuru = await _context.Duyuru.FindAsync(id);
            if (duyuru != null)
            {
                _context.Duyuru.Remove(duyuru);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DuyuruExists(int id)
        {
            return _context.Duyuru.Any(e => e.Id == id);
        }
    }
}
