using CameraShop.Core.Models.Product;
using CameraShop.Core.Repository;
using CameraShop.Infrastructure.Data;
using CameraShop.Infrastructure.Data.Models;
using CameraShop.Infrastructure.Data.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace CameraShop.Tests.ShoppingCart
{
    public class ShoppingCartTests
    {

        [Test]
        public async Task ShouldIncreaseTheAmountInTheShoppingCart()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TechRentingDbContext>()
             .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new TechRentingDbContext(optionsBuilder.Options);


            var shoppingCartService = new ShoppingCartRepository(dbContext);



            var shoppingCart = new ShoppingCart
            {
                id = 1,
                Camera = new Camera
                {
                    Id = 1,
                    Brand = "X7",
                    Model = "Sony",
                    Description = "I dont like it",
                    Price = 200,
                    Year = 200,
                    ImageUrl = "https://media.wired.com/photos/5b64db3717c26f0496f4d62d/125:94/w_1976,h_1486,c_limit/Canon-G7XII-SOURCE-Canon.jpg",
                    CategoryId = 1,
                },
                ApplicationUser = new ApplicationUser
                {
                    Id = "dasod8iaijoudaij9jiosd-1231",
                    FirstName = "Ivan",
                    LastName = "Kolev",
                    Email = "Petio@abv.bg"
                },
                ApplicationUserId = "dasod8iaijoudaij9jiosd-1231",
                CameraId = 1,
                Count = 1,
                Price = 200,

            };

            await dbContext.AddAsync(shoppingCart);
            await dbContext.SaveChangesAsync();


            Assert.AreEqual(3, shoppingCartService.IncrementCount(shoppingCart, 2));




        }
        [Test]
        public async Task ShouldReduceTheAmountOfProductsInTheCart()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TechRentingDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new TechRentingDbContext(optionsBuilder.Options);


            var shoppingCartService = new ShoppingCartRepository(dbContext);



            var shoppingCart = new ShoppingCart
            {
                id = 1,
                Camera = new Camera
                {
                    Id = 1,
                    Brand = "X7",
                    Model = "Sony",
                    Description = "I dont like it",
                    Price = 200,
                    Year = 200,
                    ImageUrl = "https://media.wired.com/photos/5b64db3717c26f0496f4d62d/125:94/w_1976,h_1486,c_limit/Canon-G7XII-SOURCE-Canon.jpg",
                    CategoryId = 1,
                },
                ApplicationUser = new ApplicationUser
                {
                    Id = "dasod8iaijoudaij9jiosd-1231",
                    FirstName = "Ivan",
                    LastName = "Kolev",
                    Email = "Petio@abv.bg"
                },
                ApplicationUserId = "dasod8iaijoudaij9jiosd-1231",
                CameraId = 1,
                Count = 10,
                Price = 200,

            };

            await dbContext.AddAsync(shoppingCart);
            await dbContext.SaveChangesAsync();


            Assert.AreEqual(9, shoppingCartService.DecrementCount(shoppingCart, 1));
        }
    }
}
