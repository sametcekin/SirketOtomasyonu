﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SirketOtomasyonu.Data;
using SirketOtomasyonu.Data.Entities;
using SirketOtomasyonu.Models.Kullanici;
using System.Data;

namespace SirketOtomasyonu.Controllers
{
    [Authorize(Roles = "Super Admin,Admin")]
    public class KullaniciController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KullaniciController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var kullaniciList = _context.Kullanici.Include(x => x.Yetki).AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                kullaniciList = kullaniciList.Where(x => x.KullaniciAdi.Contains(searchString));
            }
            var model = new List<KullaniciViewModel>();
            foreach (var item in await kullaniciList.ToListAsync())
            {
                model.Add(new KullaniciViewModel
                {
                    Id = item.Id,
                    Adi = item.Adi,
                    Soyadi = item.Soyadi,
                    Email = item.Email,
                    GirisTarihi = item.GirisTarihi,
                    IsActive = item.IsActive,
                    KullaniciAdi = item.KullaniciAdi,
                    Yetki = item.Yetki,
                    YetkiId = item.YetkiId,
                });
            }
            return View(model);
        }

        public IActionResult Create()
        {
            ViewData["YetkiId"] = new SelectList(_context.Yetki, "Id", "Adi");
            return View();
        }

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
