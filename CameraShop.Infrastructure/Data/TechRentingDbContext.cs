using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CameraShop.Infrastructure.Data
{
    using CameraShop.Infrastructure.Data.Models;
    using CameraShop.Infrastructure.Data.Models.Account;

    public class TechRentingDbContext : IdentityDbContext<ApplicationUser>
    {
        public TechRentingDbContext(DbContextOptions<TechRentingDbContext> options)
            : base(options)
        {
        }


        public DbSet<Camera> Cameras { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<ShoppingCart> ShoppingCarts { get; init; }

        public DbSet<OrderHeader> OrderHeaders { get; init; }
        public DbSet<OrderDetails> OrderDetails { get; init; }

        public DbSet<Review> Reviews { get; init; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Camera>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Cameras)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Review>(r =>
            {
                r.HasOne(u => u.ApplicationUser)
                .WithMany(r => r.Reviews)
                .OnDelete(DeleteBehavior.Restrict);

                r.HasOne(c => c.Camera)
                .WithMany(r => r.Reviews)
                .OnDelete(DeleteBehavior.Restrict);
            });


            base.OnModelCreating(builder);
        }
    }
}