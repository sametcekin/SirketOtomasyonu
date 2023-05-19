using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SirketOtomasyonu.Data;
using SirketOtomasyonu.Models.Account;
using System.Security.Claims;

namespace SirketOtomasyonu.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var kullanici = await _context.Kullanici.Include(x => x.Yetki).FirstOrDefaultAsync(x => x.KullaniciAdi == model.KullaniciAdi && x.Sifre == model.Sifre);
                if (kullanici is not null)
                {
                    List<Claim> userClaims = new()
                    {
                        new Claim(ClaimTypes.NameIdentifier, kullanici.Id.ToString()),
                        new Claim(ClaimTypes.GivenName, kullanici.KullaniciAdiSoyadi),
                        new Claim(ClaimTypes.Role, kullanici.Yetki.Adi)
                    };

                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.BeniHatirla
                    };


                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
