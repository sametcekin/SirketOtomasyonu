using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SirketOtomasyonu.Data;
using SirketOtomasyonu.Models;
using SirketOtomasyonu.Models.Home;
using System.Diagnostics;

namespace SirketOtomasyonu.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
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
                PersonselListe = await _context.Personel.OrderByDescending(x => x.Id).Take(5).Include(x => x.Birim).ToListAsync()
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}