namespace TechRentingSystem.Infrastructure
{
    using Microsoft.EntityFrameworkCore;

    using TechRentingSystem.Data;
    using TechRentingSystem.Data.Models;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDataBase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<TechRentingDbContext>();

            data.Database.Migrate();

            SeedProteinCategories(data);

            return app;
        }
        private static void SeedProteinCategories(TechRentingDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }
            data.Categories.AddRange(new[]
                                                 {
                                                     new Category{Name = "Canon"},
                                                     new Category{Name = "Nikon"},
                                                     new Category{Name = "Sony"},
                                                     new Category{Name = "Panasonic"},
                                                 });

            data.SaveChanges();
        }
    }
}
