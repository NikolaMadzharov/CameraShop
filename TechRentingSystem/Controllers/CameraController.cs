using Microsoft.AspNetCore.Mvc;

namespace TechRentingSystem.Controllers
{
    using TechRentingSystem.Data;
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

            return this.View();
        }

        private IEnumerable<CameraCategoryViewModel> GetCameraCategories() =>
            this.data.Categories.Select(x => new CameraCategoryViewModel { Id = x.Id, Name = x.Name, }).ToList();
    }
}

