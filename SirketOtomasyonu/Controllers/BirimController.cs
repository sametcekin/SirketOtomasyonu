using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SirketOtomasyonu.Data;
using SirketOtomasyonu.Data.Entities;
using SirketOtomasyonu.Models.Birim;

namespace SirketOtomasyonu.Controllers
{
    [Authorize(Roles = "Super Admin,Admin")]
    public class BirimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BirimController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var birimList = _context.Birim.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                birimList = birimList.Where(x => x.Adi.Contains(searchString) ||
                                                 x.Aciklama.Contains(searchString));
            }

            var model = new List<BirimViewModel>();
            foreach (var item in await birimList.ToListAsync())
            {
                model.Add(new BirimViewModel
                {
                    Id = item.Id,
                    Adi = item.Adi,
                    Aciklama = item.Aciklama,
                });
            }
            return View(model);
        }

        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new BirimViewModel());

            var birim = await _context.Birim.FindAsync(id);
            if (birim == null)
            {
                return NotFound();
            }
            var model = new BirimViewModel
            {
                Id = birim.Id,
                Adi = birim.Adi,
                Aciklama = birim.Aciklama
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,Adi,Aciklama")] Birim birim)
        {
            if (id != birim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    _context.Add(birim);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _context.Update(birim);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BirimExists(birim.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(birim);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Birim == null)
            {
                return NotFound();
            }

            var birim = await _context.Birim
                .FirstOrDefaultAsync(m => m.Id == id);
            if (birim == null)
            {
                return NotFound();
            }

            return View(birim);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Birim == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Birim'  is null.");
            }
            var birim = await _context.Birim.FindAsync(id);
            if (birim != null)
            {
                _context.Birim.Remove(birim);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BirimExists(int id)
        {
            return _context.Birim.Any(e => e.Id == id);
        }
    }
}
