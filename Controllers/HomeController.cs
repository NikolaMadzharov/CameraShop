using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace TechRentingSystem.Controllers
{
    using CameraShop.Core.Models;
    using CameraShop.Core.Models.Home;
    using CameraShop.Infrastructure.Data;
 
  

    public class HomeController : Controller
    {

        private readonly TechRentingDbContext data;


        public HomeController(TechRentingDbContext data)
        {
           
            this.data = data;

        }


        public IActionResult Index()
        {
          

            var cameras = this.data
                .Cameras
                .OrderByDescending(c => c.Id)
                .Select(c => new CameraIndexViewModel
                                 {
                                     Id = c.Id,
                                     Brand = c.Brand,
                                     Model = c.Model,
                                     Year = c.Year,
                                     ImageUrl = c.ImageUrl
                                 })
                .Take(3)
                .ToList();

            return View(new IndexViewModel()
                            {
                               Cameras = cameras
            });
        }
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}