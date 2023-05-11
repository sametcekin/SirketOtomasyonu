using Microsoft.AspNetCore.Mvc;
using SirketOtomasyonu.Data;
using SirketOtomasyonu.Models.Login;

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
                if (model.Username.Equals("admin") && model.Password.Equals("admin"))
                {
                    return RedirectToAction("Index","Home");
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
