namespace TechRentingSystem.Infrastructure
{
    using CameraShop.Infrastructure.Data;
    using CameraShop.Infrastructure.Data.Models;
    using Microsoft.EntityFrameworkCore;


    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDataBase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<TechRentingDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;
        }
        private static void SeedCategories(TechRentingDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }
            data.Categories.AddRange(new[]
                                                 {
                                                     new Category{Name = "Compact Cameras"},
                                                     new Category{Name = "DSLR Cameras"},
                                                     new Category{Name = "360 Cameras"},
                                                     new Category{Name = "Action (Adventure) Cameras"},
                                                 });

            data.SaveChanges();
        }

      
    }
}
