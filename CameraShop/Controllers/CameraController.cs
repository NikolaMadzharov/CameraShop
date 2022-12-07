using Microsoft.AspNetCore.Mvc;

namespace TechRentingSystem.Controllers
{
    using CameraShop.Core.Models.Enum;
    using CameraShop.Core.Models.Product;
    using CameraShop.Core.Repository.IRepository;
    using CameraShop.Infrastructure.Data;
    using CameraShop.Infrastructure.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using System.Security.Claims;


    public class CameraController : BaseController
    {
        private readonly TechRentingDbContext data;

        private readonly IUnitOfWork _unitOfWork;

        public CameraController(TechRentingDbContext data,
             IUnitOfWork unitOfWork)
        {
            this.data = data;
            _unitOfWork = unitOfWork;
        }




        [AllowAnonymous]
        public IActionResult All([FromQuery] AllCameraQueryModel query)
        {
            var camerasQuery = this.data.Cameras.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                camerasQuery = camerasQuery.Where(x => x.Brand == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.searchTerm))
            {
                camerasQuery = camerasQuery.Where(c =>
                    (c.Brand + " " + c.Model).ToLower().Contains(query.searchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(query.searchTerm.ToLower()));
            }

            camerasQuery = query.Sorting switch
            {
                CameraSorting.Year => camerasQuery.OrderByDescending(c => c.Year),
                CameraSorting.BrandAndModel => camerasQuery.OrderBy(c => c.Brand).ThenBy(c => c.Model)
            };

            var totalCameras = camerasQuery.Count();


            var cameras = camerasQuery
                .Skip((query.CurrentPage - 1) * AllCameraQueryModel.CameraPerPage)
                .Take(AllCameraQueryModel.CameraPerPage)
                .Select
                (
                x => new CameraListiningViewModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    Year = x.Year,
                    ImageUrl = x.ImageUrl,
                    Price = x.Price,
                    Category = x.Category.Name
                })
                .ToList();


            var cameraBrands = this.data
                .Cameras.Select(x => x.Brand)
                .Distinct()
                .ToList();

            query.Brands = cameraBrands;
            query.Cameras = cameras;
            query.TotalCameras = totalCameras;

            return View(query);
        }




        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int productId)
        {
            var reviews = await _unitOfWork.Review.GetAllReview(productId);


            ShoppingCart cartObj = new()
            {
                Count = 1,
                CameraId = productId,
                Camera = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Reviews"),

            };

            return View(cartObj);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.ApplicationUserId == claim.Value && u.CameraId == shoppingCart.CameraId);

            if (cartFromDb == null)
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }
            else
            {
                _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
            }

            _unitOfWork.Save();

            return RedirectToAction("Index", "Cart");
        }


    }
}