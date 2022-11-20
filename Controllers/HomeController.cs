using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechRentingSystem.Models;

namespace TechRentingSystem.Controllers
{
    using TechRentingSystem.Data;
    using TechRentingSystem.Models.Home;

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