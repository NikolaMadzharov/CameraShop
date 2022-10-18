namespace TechRentingSystem.Models.Seller
{
    using Microsoft.Build.Framework;

    public class SellerRegisterViewModel
    {
        [Required]
        public string Address { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int Phone { get; set; }
    }
}
