using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechRentingSystem.Data;
using TechRentingSystem.Data.Models;
using TechRentingSystem.Models.Cameras;
using TechRentingSystem.Models.Home;

namespace TechRentingSystem.Areas.Admin.Controllers
{
    public class CameraController : BaseController
    {
        private readonly TechRentingDbContext _data;

        public CameraController(TechRentingDbContext data)
        {
            _data = data;
        }
        public  IActionResult Index()
        {
            var allCameras =  this._data.Cameras
                .Select(x => new CameraIndexViewModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    ImageUrl = x.ImageUrl,
                    Year = x.Year
                }).ToList();

            return View(allCameras);
        }
        public IActionResult Add() => this.View(new AddCameraFromModel
        {
            Categories = this.GetCameraCategories()
        });

        private IEnumerable<CameraCategoryViewModel> GetCameraCategories() =>
       this._data.Categories.Select(x => new CameraCategoryViewModel { Id = x.Id, Name = x.Name, }).ToList();

        [HttpPost]
        public IActionResult Add(AddCameraFromModel camera)
        {
           

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
            this._data.Add(cameraData);
            this._data.SaveChanges();

            return this.RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var camera = _data.Cameras.FirstOrDefault(x => x.Id == id);
            
            _data.Remove(camera);
            _data.SaveChanges();
            return this.RedirectToAction(nameof(Index));
        }

     

    }
}
