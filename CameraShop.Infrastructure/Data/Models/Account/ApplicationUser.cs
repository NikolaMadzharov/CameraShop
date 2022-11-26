namespace CameraShop.Infrastructure.Data.Models.Account
{
    using CameraShop.Infrastructure.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();



    }
}
