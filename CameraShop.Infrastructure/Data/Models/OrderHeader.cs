using CameraShop.Infrastructure.Data.Models.Account;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CameraShop.Infrastructure.Data.Models
{
    public class OrderHeader
    {

        [Key]
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime? OrderDate { get; set; }


        [Required]
        public decimal OrderTotal { get; set; }

        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }

        public string? SessionId { get; set; }
        public string? PaymentIntendId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Emal { get; set; }
    }
}
