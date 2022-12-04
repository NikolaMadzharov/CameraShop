using CameraShop.Core.Models.Account;
using CameraShop.Core.Repository;
using CameraShop.Infrastructure.Data;
using CameraShop.Infrastructure.Data.Models.Account;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraShop.Repositories.Tests
{
    public class ApplicationUserRepositoryTests
    {
        [Test]
        public async Task ShouldCreateUserById()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TechRentingDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new TechRentingDbContext(optionsBuilder.Options);


            var applicationUserService = new ApplicationUserRepository(dbContext);

            var user = new ApplicationUser
            {
                Id = "dasod8iaijoudaij9jiosd-1231",
                FirstName = "Ivan",
                LastName = "Kolev",
                Email = "Petio@abv.bg"
               
            };

            dbContext.AddAsync(user);
            dbContext.SaveChangesAsync();

            var result =  applicationUserService.GetUserById(user.Id);

            Assert.NotNull(result);
            Assert.AreEqual("Ivan", result.Result.FirstName);
            Assert.AreEqual("dasod8iaijoudaij9jiosd-1231", result.Result.Id);
            Assert.AreEqual("Kolev", result.Result.LastName);

        }

        [Test]
        public async Task ShouldGetAllUser()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TechRentingDbContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new TechRentingDbContext(optionsBuilder.Options);


            var applicationUserService = new ApplicationUserRepository(dbContext);

            var user = new ApplicationUser
            {
                Id = "dasod8iaijoudaij9d-1231",
                FirstName = "Ivan",
                LastName = "Kolev",
                Email = "Kole@abv.bg"

            };

            var user2 = new ApplicationUser
            {
                Id = "dasod8iaijoudaij9jiosd-1231",
                FirstName = "Petio",
                LastName = "Petrov",
                Email = "Petio@abv.bg"

            };
            dbContext.AddAsync(user);
            dbContext.AddAsync(user2);
            dbContext.SaveChangesAsync();

            var result = applicationUserService.GetAll();

            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count());

        }
    }
}
