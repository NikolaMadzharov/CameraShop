using Microsoft.AspNetCore.Mvc;

namespace TechRentingSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;

    using TechRentingSystem.Data.Models.Account;
    using TechRentingSystem.Infrastructure;
    using TechRentingSystem.Models.Account;

    public class AccountController : BaseController
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
                           {
                               Email = model.Emal,
                               FirstName = model.FirstName,
                               EmailConfirmed = true,
                               LastName = model.LastName,
                               UserName = model.Emal
                           };

            var result = await this.userManager.CreateAsync(user, model.Password);
            await this.userManager.AddClaimAsync(user, new System.Security.Claims.Claim("first_name", user.FirstName));

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);

                return this.RedirectToAction("Index", "Home");
            }

            foreach (var itemError in result.Errors)
            {
                ModelState.AddModelError("",itemError.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Emal);

            if (user != null)
            {
                var result = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {

                    if (model.ReturnUrl != null)
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);

        }

        public async Task<IActionResult> Logout(string? url = null)
        {
            await signInManager.SignOutAsync();

            return this.RedirectToAction("Index", "Home");
        }

        
      
    }
}
