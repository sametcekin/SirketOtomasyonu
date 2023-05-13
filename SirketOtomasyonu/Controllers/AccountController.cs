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

        public IActionResult Index()
        {
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
        //            List<Claim> userClaims = new List<Claim>();

        //            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.Id.ToString()));
        //            userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName));
        //            userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name));
        //            userClaims.Add(new Claim(ClaimTypes.Surname, isUser.SurName.ToString()));

        //            //Veritabanımızdaki role tablosunda kullanıcı hakkında roller varsa onlarıda ekliyoruz
        //            //Farzedelim,  fcakiroglu16@outlook.com adlı email admin rolüne sahip,

        //            if (isUser.Email == "f-cakiroglu@outlook.com")
        //            {
        //                userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
        //            }
        //            //Veritabanımızdaki claim tablosunda kullanıcı hakkında claim'ler varsa onlarıda ekliyoruz.

        //            var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

        //            var authProperties = new AuthenticationProperties
        //            {
        //                IsPersistent = loginViewModel.IsRememberMe
        //            };

        //            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
        //                 new ClaimsPrincipal(claimsIdentity),
        //authProperties);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
