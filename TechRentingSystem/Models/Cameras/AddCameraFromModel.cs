namespace TechRentingSystem.Models.Cameras
{
    using System.ComponentModel.DataAnnotations;

    public class AddCameraFromModel
    {
        public int Id { get; set; }

        public string brand { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "ImageUr")]
        public string ImageUrl { get; set; }

        public int Year { get; set; }

        public string Description { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CameraCategoryViewModel> Categories { get; set; }
    }
}
