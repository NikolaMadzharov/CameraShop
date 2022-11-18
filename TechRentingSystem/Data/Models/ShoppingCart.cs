using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using TechRentingSystem.Data.Models.Account;

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
        public decimal Price { get; set; }

    }
}
