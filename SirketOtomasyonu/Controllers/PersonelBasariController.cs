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

        public IActionResult Create()
        {
            ViewData["PersonelId"] = new SelectList(_context.Personel, "Id", $"PersonelAdiSoyadi");
            return View(new PersonelBasariCreateViewModel());
        }

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

    }
}
