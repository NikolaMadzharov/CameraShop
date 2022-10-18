namespace TechRentingSystem.Data.Models.Account
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser:IdentityUser
    {

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public int Phone { get; set; }
    }
}
