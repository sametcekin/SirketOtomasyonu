using System;
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
    public class YetkiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YetkiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Yetki
        public async Task<IActionResult> Index()
        {
              return View(await _context.Yetki.ToListAsync());
        }

        // GET: Yetki/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Yetki/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yetki/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Adi")] Yetki yetki)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yetki);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yetki);
        }

        // GET: Yetki/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Yetki == null)
            {
                return NotFound();
            }

            var yetki = await _context.Yetki.FindAsync(id);
            if (yetki == null)
            {
                return NotFound();
            }
            return View(yetki);
        }

        // POST: Yetki/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Adi")] Yetki yetki)
        {
            if (id != yetki.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
                return RedirectToAction(nameof(Index));
            }
            return View(yetki);
        }

        // GET: Yetki/Delete/5
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

        // POST: Yetki/Delete/5
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
