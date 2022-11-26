using System.ComponentModel.DataAnnotations;

namespace CameraShop.Core.Models.Review
{
    public class AddReviewViewModel
    {
        [Required]
        [StringLength(500)]
        public string Comment { get; set; } = null!;

        [Range(1, 10)]
        public int Rating { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } = null!;

        public int CameraId { get; set; }

        public DateTime DateOfPublication { get; set; }
    }
}
