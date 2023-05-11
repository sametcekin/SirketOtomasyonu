﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SirketOtomasyonu.Data;
using SirketOtomasyonu.Data.Entities;

namespace SirketOtomasyonu.Controllers
{
    public class BirimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BirimController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Birim
        public async Task<IActionResult> Index()
        {
              return View(await _context.Birim.ToListAsync());
        }

        // GET: Birim/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Birim/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Birim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adi,Aciklama")] Birim birim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(birim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(birim);
        }

        // GET: Birim/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Birim == null)
            {
                return NotFound();
            }

            var birim = await _context.Birim.FindAsync(id);
            if (birim == null)
            {
                return NotFound();
            }
            return View(birim);
        }

        // POST: Birim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi,Aciklama")] Birim birim)
        {
            if (id != birim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
                return RedirectToAction(nameof(Index));
            }
            return View(birim);
        }

        // GET: Birim/Delete/5
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

        // POST: Birim/Delete/5
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
