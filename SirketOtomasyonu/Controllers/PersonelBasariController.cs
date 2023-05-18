using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SirketOtomasyonu.Data;
using SirketOtomasyonu.Data.Entities;
using SirketOtomasyonu.Models.PersonelBasari;
using System.Data;

namespace SirketOtomasyonu.Controllers
{
    [Authorize(Roles = "Super Admin,Admin")]
    public class PersonelBasariController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonelBasariController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PersonelBasari
        public async Task<IActionResult> Index(int personelId = 0)
        {
            ViewData["CurrentFilter"] = personelId;
            var liste = new PersonelBasariViewModel();

            var personelBasariList = _context.PersonelBasari.Include(p => p.Personel).AsQueryable();
            if (personelId is not 0)
            {
                personelBasariList = personelBasariList.Where(x => x.PersonelId == personelId);
                foreach (var item in await personelBasariList.ToListAsync())
                {
                    liste.PersonelBasariListe.Add(item);
                }
            }
            liste.PersonelListe = await _context.Personel.Select(x => new SelectListItem { Text = $"{x.Id} - {x.PersonelAdiSoyadi}", Value = x.Id.ToString() }).ToListAsync();
            return View(liste);
        }

        //[HttpGet("{personelId}")]
        public async Task<IActionResult> GetPersonelBasariByPersonelId(int personelId = 0)
        {
            var liste = new PersonelBasariViewModel();

            var personelBasariList = _context.PersonelBasari.Include(p => p.Personel).AsQueryable();

            if (personelId is not 0)
            {
                personelBasariList = personelBasariList.Where(x => x.PersonelId == personelId);
                foreach (var item in await personelBasariList.ToListAsync())
                {
                    liste.PersonelBasariListe.Add(item);
                }
            }
            liste.PersonelId = personelId;
            return Json(liste);
        }


        // GET: PersonelBasari/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PersonelBasari == null)
            {
                return NotFound();
            }

            var personelBasari = await _context.PersonelBasari
                .Include(p => p.Personel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personelBasari == null)
            {
                return NotFound();
            }

            return View(personelBasari);
        }

        // GET: PersonelBasari/Create
        public IActionResult Create()
        {
            ViewData["PersonelId"] = new SelectList(_context.Personel, "Id", $"PersonelAdiSoyadi");
            return View();
        }

        // POST: PersonelBasari/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonelId,Aciklama,Tarih")] PersonelBasari personelBasari)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personelBasari);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonelId"] = new SelectList(_context.Personel, "Id", "Id", personelBasari.PersonelId);
            return View(personelBasari);
        }

        // GET: PersonelBasari/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PersonelBasari == null)
            {
                return NotFound();
            }

            var personelBasari = await _context.PersonelBasari.FindAsync(id);
            if (personelBasari == null)
            {
                return NotFound();
            }
            ViewData["PersonelId"] = new SelectList(_context.Personel, "Id", "Adi", personelBasari.PersonelId);
            return View(personelBasari);
        }

        // POST: PersonelBasari/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonelId,Aciklama,Tarih")] PersonelBasari personelBasari)
        {
            if (id != personelBasari.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personelBasari);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonelBasariExists(personelBasari.Id))
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
            ViewData["PersonelId"] = new SelectList(_context.Personel, "Id", "Id", personelBasari.PersonelId);
            return View(personelBasari);
        }

        // GET: PersonelBasari/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PersonelBasari == null)
            {
                return NotFound();
            }

            var personelBasari = await _context.PersonelBasari
                .Include(p => p.Personel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personelBasari == null)
            {
                return NotFound();
            }

            return View(personelBasari);
        }

        // POST: PersonelBasari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PersonelBasari == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PersonelBasari'  is null.");
            }
            var personelBasari = await _context.PersonelBasari.FindAsync(id);
            if (personelBasari != null)
            {
                _context.PersonelBasari.Remove(personelBasari);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonelBasariExists(int id)
        {
            return _context.PersonelBasari.Any(e => e.Id == id);
        }
    }
}
