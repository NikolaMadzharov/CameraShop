using CameraShop.Core.Models.Product;
using CameraShop.Core.Repository;
using CameraShop.Infrastructure.Data;
using CameraShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace CameraShop.Repositories.Tests
{
    public class CameraRepository
    {
        [Test]
        public async Task CreateMethodShouldAddCameraToTheDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TechRentingDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new TechRentingDbContext(optionsBuilder.Options);

            var cameraService = new ProductRepositry(dbContext);

            var CameraToAdd = new AddCameraFromModel
            {
                Id = 1,
                Brand = "X7",
                Model = "Sony",
                Description = "I dont like it",
                Price = 200,
                Year = 200,
                ImageUrl = "https://media.wired.com/photos/5b64db3717c26f0496f4d62d/125:94/w_1976,h_1486,c_limit/Canon-G7XII-SOURCE-Canon.jpg",
                CategoryId = 1,

            };

            await cameraService.Add(CameraToAdd);

            Assert.NotNull(dbContext.Cameras.FirstOrDefaultAsync());
            Assert.AreEqual("Sony", dbContext.Cameras.FirstAsync().Result.Model);
            Assert.AreEqual("X7", dbContext.Cameras.FirstAsync().Result.Brand);
            Assert.AreEqual(200, dbContext.Cameras.FirstAsync().Result.Price);
            Assert.AreEqual(1, dbContext.Cameras.FirstAsync().Result.CategoryId);
            Assert.AreEqual(1, dbContext.Cameras.FirstAsync().Result.Id);
            Assert.AreEqual("I dont like it", dbContext.Cameras.FirstAsync().Result.Description);
            Assert.AreEqual(200, dbContext.Cameras.FirstAsync().Result.Year);

        }
        [Test]
        public async Task RemoveCameraFromDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TechRentingDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new TechRentingDbContext(optionsBuilder.Options);

            var cameraService = new ProductRepositry(dbContext);

            var CameraToAdd = new AddCameraFromModel
            {
                Id = 1,
                Brand = "X7",
                Model = "Sony",
                Description = "I dont like it",
                Price = 200,
                Year = 200,
                ImageUrl = "https://media.wired.com/photos/5b64db3717c26f0496f4d62d/125:94/w_1976,h_1486,c_limit/Canon-G7XII-SOURCE-Canon.jpg",
                CategoryId = 1,

            };

            await cameraService.Add(CameraToAdd);

            cameraService.Delete(1);

            Assert.AreEqual(0, dbContext.Cameras.Count());



        }
        [Test]
        public async Task ShouldCheckIfYouAddCategoriesProperly()
        {

            var optionsBuilder = new DbContextOptionsBuilder<TechRentingDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new TechRentingDbContext(optionsBuilder.Options);

            var cameraService = new ProductRepositry(dbContext);

            var category = new Category
            {
                Id = 1,
                Name = "Sony"
            };

            dbContext.AddRangeAsync(category);
            dbContext.SaveChangesAsync();

            Assert.AreEqual(1, dbContext.Categories.FirstAsync().Result.Id);
            Assert.AreEqual("Sony", dbContext.Categories.FirstAsync().Result.Name);
        }

        [Test]
        public async Task ShouldGetAllCamerasCategories()
        {

            var optionsBuilder = new DbContextOptionsBuilder<TechRentingDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new TechRentingDbContext(optionsBuilder.Options);

            var cameraService = new ProductRepositry(dbContext);

            var category = new Category
            {
                Id = 1,
                Name = "Sony"
            };

            dbContext.AddRangeAsync(category);
            dbContext.SaveChangesAsync();

            var result = cameraService.GetCameraCategories();

            Assert.NotNull(result);
            Assert.AreEqual(1, result.Result.FirstOrDefault().Id);
            Assert.AreEqual("Sony", result.Result.FirstOrDefault().Name);

        }
        [Test]
        public async Task ShouldGetAllCameras()
        {

            var optionsBuilder = new DbContextOptionsBuilder<TechRentingDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new TechRentingDbContext(optionsBuilder.Options);

            var cameraService = new ProductRepositry(dbContext);


            var CameraToAdd = new AddCameraFromModel
            {
                Id = 1,
                Brand = "X7",
                Model = "Sony",
                Description = "I dont like it",
                Price = 200,
                Year = 200,
                ImageUrl = "https://media.wired.com/photos/5b64db3717c26f0496f4d62d/125:94/w_1976,h_1486,c_limit/Canon-G7XII-SOURCE-Canon.jpg",
                CategoryId = 1,

            };

            await cameraService.Add(CameraToAdd);

            var result = cameraService.GetCamerasAsync();

            Assert.NotNull(result);
            Assert.AreEqual("Sony", result.Result.FirstOrDefault().Model);
            Assert.AreEqual("X7", result.Result.FirstOrDefault().Brand);
            Assert.AreEqual(200, result.Result.FirstOrDefault().Year);
            Assert.AreEqual(1, result.Result.FirstOrDefault().Id);

        }
    }
}