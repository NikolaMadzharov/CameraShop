namespace TechRentingSystem.Models.Cameras
{
    using System.ComponentModel.DataAnnotations;

    public class AddCameraFromModel
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Url]
        [Display(Name = "ImageUr")]
        public string ImageUrl { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Description { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CameraCategoryViewModel> Categories { get; set; }
    }
}
