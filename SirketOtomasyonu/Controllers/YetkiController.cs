using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SirketOtomasyonu.Data;
using SirketOtomasyonu.Data.Entities;
using SirketOtomasyonu.Models.Yetki;
using System.Data;

namespace SirketOtomasyonu.Controllers
{
    [Authorize(Roles = "Super Admin,Admin")]
    public class YetkiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YetkiController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var yetkiList = _context.Yetki.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                yetkiList = yetkiList.Where(x => x.Adi.Contains(searchString));
            }
            var model = new List<YetkiViewModel>();
            foreach (var item in await yetkiList.ToListAsync())
            {
                model.Add(new YetkiViewModel
                {
                    Id = item.Id,
                    Adi = item.Adi,
                });
            }
            return View(model);
        }

        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new YetkiViewModel());
            }

            var yetki = await _context.Yetki.FindAsync(id);
            if (yetki == null)
            {
                return NotFound();
            }

            var model = new YetkiViewModel
            {
                Id = yetki.Id,
                Adi = yetki.Adi,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,Adi")] Yetki yetki)
        {
            if (id != yetki.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    _context.Add(yetki);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _context.Update(yetki);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!YetkiExists(yetki.Id))
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
            return View(yetki);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Yetki == null)
            {
                return NotFound();
            }

            var yetki = await _context.Yetki
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yetki == null)
            {
                return NotFound();
            }

            return View(yetki);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Yetki == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Yetki'  is null.");
            }
            var yetki = await _context.Yetki.FindAsync(id);
            if (yetki != null)
            {
                _context.Yetki.Remove(yetki);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YetkiExists(int id)
        {
            return _context.Yetki.Any(e => e.Id == id);
        }
    }
}
