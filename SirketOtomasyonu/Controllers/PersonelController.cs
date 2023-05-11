using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SirketOtomasyonu.Data;
using SirketOtomasyonu.Data.Entities;
using SirketOtomasyonu.Models.Personel;

namespace SirketOtomasyonu.Controllers
{
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
            var personelList = await _context.Personel.Include(x => x.Birim).ToListAsync();
            var personelModel = new List<PersonelViewModel>();
            foreach (var personel in personelList)
            {
                personelModel.Add(new PersonelViewModel
                {
                    Id = personel.Id,
                    Aciklama = personel.Aciklama,
                    Adi = personel.Adi,
                    Adres = personel.Adres,
                    Birim = personel.Birim.Adi,
                    Email = personel.Email,
                    GirisTarihi = personel.GirisTarihi.ToShortDateString(),
                    IsActive = personel.IsActive,
                    Maas = personel.Maas,
                    Resim = personel.Resim,
                    Soyadi = personel.Soyadi,
                    Tel1 = personel.Tel1,
                    Tel2 = personel.Tel2,
                });
            }
            return View(personelModel);
        }

        // GET: Personel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personel == null)
            {
                return NotFound();
            }

            var personel = await _context.Personel.Include(x => x.Birim)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personel == null)
            {
                return NotFound();
            }
            var model = new PersonelViewModel
            {
                Id = personel.Id,
                Aciklama = personel.Aciklama,
                Adi = personel.Adi,
                Adres = personel.Adres,
                Birim = personel.Birim?.Adi,
                Email = personel.Email,
                GirisTarihi = personel.GirisTarihi.ToShortDateString(),
                IsActive = personel.IsActive,
                Maas = personel.Maas,
                Resim = personel.Resim,
                Soyadi = personel.Soyadi,
                Tel1 = personel.Tel1,
                Tel2 = personel.Tel2,
            };
            return View(model);
        }

        // GET: Personel/Create
        public IActionResult Create()
        {
            var birimler = _context.Birim.ToList();

            var model = new PersonelCreateViewModel();
            model.Birimler = new List<SelectListItem>();

            foreach (var birim in birimler)
            {
                model.Birimler.Add(new SelectListItem { Text = birim.Adi, Value = birim.Id.ToString() });
            }
            return View(model);
        }

        // POST: Personel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BirimId,Adi,Soyadi,Tel1,Tel2,Email,Adres,Maas,Resim,Aciklama,IsActive,GirisTarihi")] Personel personel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personel);
        }

        // GET: Personel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personel == null)
            {
                return NotFound();
            }

            var personel = await _context.Personel.FindAsync(id);
            if (personel == null)
            {
                return NotFound();
            }

            var birimler = await _context.Birim.ToListAsync();

            var model = new PersonelUpdateViewModel
            {
                Id = personel.Id,
                Aciklama = personel.Aciklama,
                Adi = personel.Adi,
                Adres = personel.Adres,
                BirimId = personel.BirimId.ToString(),
                Email = personel.Email,
                GirisTarihi = personel.GirisTarihi,
                IsActive = personel.IsActive,
                Maas = personel.Maas,
                Resim = personel.Resim,
                Soyadi = personel.Soyadi,
                Tel1 = personel.Tel1,
                Tel2 = personel.Tel2,
            };
            model.Birimler = new List<SelectListItem>();
            foreach (var birim in birimler)
            {
                model.Birimler.Add(new SelectListItem { Text = birim.Adi, Value = birim.Id.ToString() });
            }

            return View(model);
        }

        // POST: Personel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BirimId,Adi,Soyadi,Tel1,Tel2,Email,Adres,Maas,Resim,Aciklama,IsActive,GirisTarihi")] Personel personel)
        {
            if (id != personel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonelExists(personel.Id))
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
            return View(personel);
        }

        // GET: Personel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personel == null)
            {
                return NotFound();
            }

            var personel = await _context.Personel.Include(x => x.Birim)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personel == null)
            {
                return NotFound();
            }

            var model = new PersonelViewModel
            {
                Id = personel.Id,
                Aciklama = personel.Aciklama,
                Adi = personel.Adi,
                Adres = personel.Adres,
                Birim = personel.Birim?.Adi,
                Email = personel.Email,
                GirisTarihi = personel.GirisTarihi.ToShortDateString(),
                IsActive = personel.IsActive,
                Maas = personel.Maas,
                Resim = personel.Resim,
                Soyadi = personel.Soyadi,
                Tel1 = personel.Tel1,
                Tel2 = personel.Tel2,
            };

            return View(model);
        }

        // POST: Personel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Personels'  is null.");
            }
            var personel = await _context.Personel.FindAsync(id);
            if (personel != null)
            {
                _context.Personel.Remove(personel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonelExists(int id)
        {
            return _context.Personel.Any(e => e.Id == id);
        }
    }
}
