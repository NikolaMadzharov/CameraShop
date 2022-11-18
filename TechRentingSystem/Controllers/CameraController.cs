using Microsoft.AspNetCore.Mvc;

namespace TechRentingSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;
    using TechRentingSystem.Contracts;
    using TechRentingSystem.Data;
    using TechRentingSystem.Data.Models;
    using TechRentingSystem.Models.Cameras;
    using TechRentingSystem.Models.Enum;
    using TechRentingSystem.Models.Product;
    using TechRentingSystem.Repository.IRepository;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
    
    public class CameraController : BaseController
    {
        private readonly TechRentingDbContext data;
       
        private readonly IUnitOfWork _unitOfWork;

        public CameraController(TechRentingDbContext data, IUnitOfWork unitOfWork)
        {
            this.data = data;
           _unitOfWork = unitOfWork;
        }


        public IActionResult Add() => this.View(new AddCameraFromModel
                                                    {
                                                        Categories = this.GetCameraCategories()
                                                    });

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

        [HttpPost]
        public IActionResult Add(AddCameraFromModel camera)
        {
            //if (!ModelState.IsValid)
            //{
            //    camera.Categories = this.GetCameraCategories();

            //    return this.View(camera);
            //}

            var cameraData = new Camera
                                 {
                                     Brand = camera.Brand,
                                     Model = camera.Model,
                                     Description = camera.Description,
                                     ImageUrl = camera.ImageUrl,
                                     Year = camera.Year,
                                     Price = camera.Price,
                                     CategoryId = camera.CategoryId
                                 };
            this.data.Add(cameraData);
            this.data.SaveChanges();

            return this.RedirectToAction(nameof(this.All));
        }

        private IEnumerable<CameraCategoryViewModel> GetCameraCategories() =>
            this.data.Categories.Select(x => new CameraCategoryViewModel { Id = x.Id, Name = x.Name, }).ToList();

        [HttpGet]
        public async Task<IActionResult> Details(int productId)
        {

            var data = await GetProductDetails(productId);

            return this.View(data);
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

            return RedirectToAction(nameof(Index));
        }


        public async Task<ProductDetailsModel> GetProductDetails(int productId)
        {
            var product = await data.Cameras.FirstOrDefaultAsync(x => x.Id == productId);
            var model = new ProductDetailsModel()
            {
                Id = productId,
                Brand = product.Brand,
                Model = product.Model,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Count = 1
            };


            return model;

        }
    }
}

