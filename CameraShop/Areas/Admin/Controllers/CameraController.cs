using CameraShop.Core.Models.Product;
using CameraShop.Core.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace TechRentingSystem.Areas.Admin.Controllers
{
    public class CameraController : BaseController
    {
   
        private readonly IUnitOfWork _unitOfWork;

        public CameraController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Return to the admin page(index).
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allCameras = await this._unitOfWork.Product.GetCamerasAsync();
           
            return View(allCameras);
        }
        public async Task<IActionResult> Add() => this.View(new AddCameraFromModel
        {
            Categories = await this._unitOfWork.Product.GetCameraCategories()
        });

        // Redirect to Add view where you can add camera.
        [HttpPost]
        public async Task<IActionResult> Add(AddCameraFromModel camera)
        {

            await this._unitOfWork.Product.Add(camera);

            return this.RedirectToAction(nameof(Index));
        }

        // Delete the current camera from the store.
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            await this._unitOfWork.Product.Delete(id);

            return this.RedirectToAction(nameof(Index));
        }

     

    }
}
