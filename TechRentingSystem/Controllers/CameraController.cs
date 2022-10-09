using Microsoft.AspNetCore.Mvc;

namespace TechRentingSystem.Controllers
{
    using TechRentingSystem.Data;
    using TechRentingSystem.Data.Models;
    using TechRentingSystem.Models.Cameras;

    public class CameraController : Controller
    {
        private readonly TechRentingDbContext data;

        public CameraController(TechRentingDbContext data) => this.data = data;


        public IActionResult Add() => this.View(new AddCameraFromModel
                                                    {
                                                        Categories = this.GetCameraCategories()
                                                    });

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

            return this.RedirectToAction("Index", "Home");
        }

        private IEnumerable<CameraCategoryViewModel> GetCameraCategories() =>
            this.data.Categories.Select(x => new CameraCategoryViewModel { Id = x.Id, Name = x.Name, }).ToList();
    }
}

