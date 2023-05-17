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

        public IActionResult Index()
        {
            var model = new HomeViewModel();
            model.TotalAdminSayisi = _context.Kullanici.Count(x => x.YetkiId == 1 || x.YetkiId == 2);
            model.TotalKullaniciSayisi = _context.Kullanici.Count();
            model.EnDusukMaas= _context.Personel.Min(x=>x.Maas);
            model.EnYuksekMaas= _context.Personel.Max(x=>x.Maas);
            model.PersonselListe = _context.Personel.OrderByDescending(x=>x.Id).Take(5).Include(x=>x.Birim).ToList();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}