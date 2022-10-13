using Microsoft.AspNetCore.Mvc;

namespace TechRentingSystem.Controllers
{
    using TechRentingSystem.Data;
    using TechRentingSystem.Data.Models;
    using TechRentingSystem.Models.Cameras;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

    public class CameraController : Controller
    {
        private readonly TechRentingDbContext data;

        public CameraController(TechRentingDbContext data) => this.data = data;


        public IActionResult Add() => this.View(new AddCameraFromModel
                                                    {
                                                        Categories = this.GetCameraCategories()
                                                    });


        public IActionResult All(string brand,string searchTerm)
        {
            var camerasQuery = this.data.Cameras.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                camerasQuery = camerasQuery.Where(x => x.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                camerasQuery = camerasQuery.Where(c =>
                    (c.Brand + " " + c.Model).ToLower().Contains(searchTerm.ToLower()) ||
                    c.Description.ToLower().Contains(searchTerm.ToLower()));
            }

           


            var cameras = camerasQuery.Select(
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

            return View(new AllCameraQueryModel
                                 {
                                     Brands = cameraBrands,
                                     Cameras = cameras,
                                     searchTerm = searchTerm
                                 });
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
    }
}

