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
                FirstName = "Petio",
                LastName = "Petrov",
                Email = "Petio@abv.bg"

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

            var result = applicationUserService.GetUsers();

            Assert.NotNull(result);
            Assert.AreEqual(2, result.Result.Count());
            Assert.IsTrue(result.Result.Any());
            Assert.AreEqual("Petio@abv.bg", result.Result.FirstOrDefault().Email);
            Assert.AreEqual($"Petio Petrov", result.Result.FirstOrDefault().FullName);

        }
        [Test]
        public async Task ShouldGetUserForEdit()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TechRentingDbContext>()
         .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new TechRentingDbContext(optionsBuilder.Options);


            var applicationUserService =  new ApplicationUserRepository(dbContext);


            var user = new ApplicationUser
            {
                Id = "dasod8iaijoudaij9d-12311",
                FirstName = "Petio",
                LastName = "Petrov",
                Email = "Petio@abv.bg"

            };

          await dbContext.AddAsync(user);
          await dbContext.SaveChangesAsync();

           var result = await applicationUserService.GetUserForEdit(user.Id);

            Assert.NotNull(result);
            Assert.AreEqual("Petio", result.FirstName);
            Assert.AreEqual("dasod8iaijoudaij9d-12311", result.Id);
            Assert.AreEqual("Petrov", result.LastName);
            Assert.AreEqual("Petio@abv.bg", result.Email);



        }
        [Test]
        public async Task ShouldGetUserProfile()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TechRentingDbContext>()
       .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new TechRentingDbContext(optionsBuilder.Options);


            var applicationUserService = new ApplicationUserRepository(dbContext);


            var user = new ApplicationUser
            {
                Id = "dasod8iaijoudaij9d-12311",
                FirstName = "Petio",
                LastName = "Petrov",
                Email = "Petio@abv.bg"

            };

            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();

            var result = await applicationUserService.GetUserProfile(user.Id);

            Assert.NotNull(result);
            Assert.AreEqual("Petio", result.FirstName);
            Assert.AreEqual("dasod8iaijoudaij9d-12311", result.Id);
            Assert.AreEqual("Petrov", result.LastName);
            Assert.AreEqual("Petio@abv.bg", result.Email);

        }

    }
}
