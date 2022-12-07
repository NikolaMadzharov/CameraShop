using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CameraShop.Core.Models.Product
{
    public class CameraListiningViewModel
    {

        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public int Year { get; set; }

        public bool IsActive { get; set; }
        public string Category { get; set; }
    }
}
