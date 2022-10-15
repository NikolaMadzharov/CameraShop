using Microsoft.AspNetCore.Mvc;

namespace TechRentingSystem.Controllers
{
    using Microsoft.AspNetCore.Identity;

    using TechRentingSystem.Data.Models.Account;
    using TechRentingSystem.Models.Account;

    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;

        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager =_userManager;
            signInManager = _signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
                           {
                               Email = model.Emal,
                               FirstName = model.FIrstName,
                               EmailConfirmed = true,
                               LastName = model.LastName,
                               UserName = model.Emal
                           };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);

                return this.RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(null, "Something went wrong");

            return View(model);
        }
        public async Task<IActionResult> Login(string? url = null)
        {
            return this.Ok();
        }

        public async Task<IActionResult> Logout(string? url = null)
        {
            return this.Ok();
        }
    }
}
