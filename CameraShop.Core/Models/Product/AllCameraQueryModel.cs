namespace CameraShop.Core.Models.Product
{
    using CameraShop.Core.Models.Enum;
    using System.ComponentModel.DataAnnotations;

    public class AllCameraQueryModel
    {

        public const int CameraPerPage = 3;

        public string Brand { get; set; }

        public IEnumerable<string> Brands { get; set; }

        [Display(Name = "Search")]
        public string searchTerm { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalCameras { get; set; }

        public CameraSorting Sorting { get; set; }

        public IEnumerable<CameraListiningViewModel> Cameras { get; set; }
    }
}
