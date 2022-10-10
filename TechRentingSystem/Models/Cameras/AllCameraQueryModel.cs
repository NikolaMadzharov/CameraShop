namespace TechRentingSystem.Models.Cameras
{
    using System.ComponentModel.DataAnnotations;

    using TechRentingSystem.Models.Enum;

    public class AllCameraQueryModel
    {
        public IEnumerable<string> Brands { get; set; }
        [Display(Name = "Search")]
        public string searchTerm { get; set; }

        public CameraSorting Sorting { get; set; }

        public IEnumerable<CameraListiningViewModel> Cameras { get; set; }
    }
}
