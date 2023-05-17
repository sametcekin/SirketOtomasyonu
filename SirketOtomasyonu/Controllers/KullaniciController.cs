using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SirketOtomasyonu.Data;
using SirketOtomasyonu.Data.Entities;

namespace SirketOtomasyonu.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KullaniciController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kullanicis
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var kullaniciList = _context.Kullanici.Include(x => x.Yetki).AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                kullaniciList = kullaniciList.Where(x => x.KullaniciAdi.Contains(searchString));
            }
            return View(await kullaniciList.ToListAsync());
        }

        // GET: Kullanicis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kullanici == null)
            {
                return NotFound();
            }

            var kullanici = await _context.Kullanici
                .Include(k => k.Yetki)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kullanici == null)
            {
                return NotFound();
            }

            return View(kullanici);
        }

        // GET: Kullanicis/Create
        public IActionResult Create()
        {
            ViewData["YetkiId"] = new SelectList(_context.Yetki, "Id", "Adi");
            return View();
        }

        // POST: Kullanicis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,YetkiId,KullaniciAdi,Sifre,Adi,Soyadi,Email,IsActive,GirisTarihi")] Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kullanici);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["YetkiId"] = new SelectList(_context.Yetki, "Id", "Adi", kullanici.YetkiId);
            return View(kullanici);
        }

        // GET: Kullanicis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kullanici == null)
            {
                return NotFound();
            }

            var kullanici = await _context.Kullanici.FindAsync(id);
            if (kullanici == null)
            {
                return NotFound();
            }
            ViewData["YetkiId"] = new SelectList(_context.Yetki, "Id", "Adi", kullanici.YetkiId);
            return View(kullanici);
        }

        // POST: Kullanicis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,YetkiId,KullaniciAdi,Sifre,Adi,Soyadi,Email,IsActive,GirisTarihi")] Kullanici kullanici)
        {
            if (id != kullanici.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kullanici);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KullaniciExists(kullanici.Id))
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
            ViewData["YetkiId"] = new SelectList(_context.Yetki, "Id", "Id", kullanici.YetkiId);
            return View(kullanici);
        }

        // GET: Kullanicis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kullanici == null)
            {
                return NotFound();
            }

            var kullanici = await _context.Kullanici
                .Include(k => k.Yetki)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kullanici == null)
            {
                return NotFound();
            }

            return View(kullanici);
        }

        // POST: Kullanicis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kullanici == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Kullanici'  is null.");
            }
            var kullanici = await _context.Kullanici.FindAsync(id);
            if (kullanici != null)
            {
                _context.Kullanici.Remove(kullanici);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KullaniciExists(int id)
        {
            return _context.Kullanici.Any(e => e.Id == id);
        }
    }
}
