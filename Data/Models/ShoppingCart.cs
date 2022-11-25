using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using TechRentingSystem.Data.Models.Account;
using TechRentingSystem.Models.Review;

namespace TechRentingSystem.Data.Models
{
    public class ShoppingCart
    {
        public int id { get; set; }

        public int CameraId { get; set; }
        [ForeignKey("CameraId")]
        [ValidateNever]
        public Camera Camera { get; set; }

        public int Count { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [NotMapped]
        public double Price { get; set; }

        public ICollection<ReviewViewModel> Reviews { get; set; } = new HashSet<ReviewViewModel>();

    }
}
