using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SirketOtomasyonu.Data;
using SirketOtomasyonu.Data.Entities;
using SirketOtomasyonu.Models.Login;
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

        [HttpGet]
        public async Task<IActionResult> LogOut(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var kullanici = new Kullanici
                //{
                //    Id=1,
                //    Adi="Samet",
                //    Soyadi="Cekin",
                //    GirisTarihi=
                //}
                if (model.Username.Equals("admin") && model.Password.Equals("admin"))
                {
                    List<Claim> userClaims = new();

                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, "samet"));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, "Samet Cekin"));
                    userClaims.Add(new Claim(ClaimTypes.Surname, "Cekin"));

                    userClaims.Add(new Claim(ClaimTypes.Role, "admin"));

                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };


                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                          new ClaimsPrincipal(claimsIdentity),
         authProperties);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Login","Account");
        }

    }
}
