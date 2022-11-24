using Stripe;
using System.ComponentModel.DataAnnotations;
using TechRentingSystem.Data.Models.Account;

namespace TechRentingSystem.Data.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; } = null!;

        [MaxLength(5)]
        public int Rating { get; set; }

      

        public DateTime DateOfPublication { get; set; }

        public DateTime? LastUpdate { get; set; }

        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public int CameraId { get; set; }
        public Camera Camera { get; set; } = null!;
    }
}
