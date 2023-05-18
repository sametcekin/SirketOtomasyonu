using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SirketOtomasyonu.Data;
using SirketOtomasyonu.Models.Home;

namespace SirketOtomasyonu.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                TotalAdminSayisi = _context.Kullanici.Count(x => x.YetkiId == 1 || x.YetkiId == 2),
                TotalKullaniciSayisi = _context.Kullanici.Count(),
                EnDusukMaas = _context.Personel.Min(x => x.Maas),
                EnYuksekMaas = _context.Personel.Max(x => x.Maas),
                PersonselListe = await _context.Personel.OrderByDescending(x => x.Id).Take(5).Include(x => x.Birim).ToListAsync(),
                DuyuruListe = await _context.Duyuru.OrderByDescending(x => x.Id).Take(5).ToListAsync(),
            };
            return View(model);
        }
    }
}