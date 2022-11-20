using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechRentingSystem.Data.Models.Account;

namespace TechRentingSystem.Data.Models
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
        public double OrderTotal { get; set; }
     
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
