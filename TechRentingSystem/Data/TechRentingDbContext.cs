using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TechRentingSystem.Data
{
    using TechRentingSystem.Data.Models;
    using TechRentingSystem.Data.Models.Account;

    public class TechRentingDbContext : IdentityDbContext<ApplicationUser>
    {
        public TechRentingDbContext(DbContextOptions<TechRentingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Camera> Cameras { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<ShoppingCart> ShoppingCarts { get; init; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Camera>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Cameras)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}