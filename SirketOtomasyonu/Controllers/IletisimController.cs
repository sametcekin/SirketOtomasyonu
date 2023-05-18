using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SirketOtomasyonu.Data;
using SirketOtomasyonu.Data.Entities;

namespace SirketOtomasyonu.Controllers
{
    [Authorize]
    public class IletisimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IletisimController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdiSoyadi,Email,Baslik,Mesaj")] Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                iletisim.Tarih = DateTime.Now;
                _context.Add(iletisim);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create");
            }
            return View(iletisim);
        }
    }
}
