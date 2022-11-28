using CameraShop.Core.Models.Account;
using CameraShop.Core.Repository.IRepository;
using CameraShop.Infrastructure.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
namespace TechRentingSystem.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(UserManager<ApplicationUser> userManager,
                              IUnitOfWork unitOfWork,
                              RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this._unitOfWork = unitOfWork;
            this.roleManager = roleManager;
        }

      
        public async Task<IActionResult> Index()
        {
            var model = await _unitOfWork.ApplicationUser.GetUsers();
            return View(model);
        }

      
        public async Task<IActionResult> Roles(string id)
        {
            var user = await _unitOfWork.ApplicationUser.GetUserById(id);
            var model = new UserRolesViewModel()
            {
                UserId = user.Id,
                Name = $"{user.FirstName} {user.LastName}"
            };


            ViewBag.RoleItems = roleManager.Roles
                .ToList()
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList();

            return View(model);
        }
       
        [HttpPost]
        public async Task<IActionResult> Roles(UserRolesViewModel model)
        {
            var user = await _unitOfWork.ApplicationUser.GetUserById(model.UserId);
            var userRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles);

            if (model.RoleNames?.Length > 0)
            {
                await userManager.AddToRolesAsync(user, model.RoleNames);
            }

            return RedirectToAction(nameof(Index));
        }

      
        public async Task<IActionResult> Edit()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await _unitOfWork.ApplicationUser.GetUserForEdit(userId);

            return View(model);
        }

      
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            model.Id = userId;



            await _unitOfWork.ApplicationUser.UpdateUser(model);

            return RedirectToAction("Index", "User");
        }

    }
}
