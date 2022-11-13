using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TechRentingSystem.Models.Cameras
{
    public class CameraListiningViewModel
    {

        public int Id { get; set; }

        public string Brand { get; set; }
     
        public string Model { get; set; }
   
        public decimal Price { get; set; }
        
        public string ImageUrl { get; set; }
   
        public int Year { get; set; }

        public string Category { get; set; }
    }
}
