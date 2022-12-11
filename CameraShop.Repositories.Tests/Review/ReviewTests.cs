using CameraShop.Core.Models.Product;
using CameraShop.Core.Models.Review;
using CameraShop.Core.Repository;
using CameraShop.Infrastructure.Data;
using CameraShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraShop.Tests.Review
{
    public class ReviewTests
    {
        [Test]
        public async Task ShouldAddReviewToTheDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TechRentingDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new TechRentingDbContext(optionsBuilder.Options);

            var reviewService = new ReviewRepository(dbContext);


            var user = new CameraShop.Infrastructure.Data.Models.Account.ApplicationUser
            {
                Id = "dasod8iaijoudaij9d-1231",
                FirstName = "Petio",
                LastName = "Petrov",
                Email = "Petio@abv.bg"

            };


            await dbContext.AddAsync(user);

            var Camera = new CameraShop.Infrastructure.Data.Models.Camera
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
            await dbContext.AddAsync(Camera);

            var review = new AddReviewViewModel
            {
                ApplicationUserId = user.Id,
                Comment = "i like it",
                Rating = 4,
                CameraId = Camera.Id,
                
               
            };

         
            await reviewService.AddReview(review);

            Assert.NotNull(dbContext.Reviews.FirstOrDefaultAsync());
            Assert.AreEqual(user.Id,dbContext.Reviews.FirstOrDefaultAsync().Result.ApplicationUserId);
            Assert.AreEqual(review.Comment, dbContext.Reviews.FirstOrDefaultAsync().Result.Comment);
            Assert.AreEqual(review.Rating, dbContext.Reviews.FirstOrDefaultAsync().Result.Rating);
            Assert.AreEqual(Camera.Id, dbContext.Reviews.FirstOrDefaultAsync().Result.CameraId);
        }

        [Test]
        public async Task ShouldGetAllReview()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TechRentingDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new TechRentingDbContext(optionsBuilder.Options);

            var reviewService = new ReviewRepository(dbContext);


            var user = new CameraShop.Infrastructure.Data.Models.Account.ApplicationUser
            {
                Id = "dasod8iaijoudaij9d-1231",
                FirstName = "Petio",
                LastName = "Petrov",
                Email = "Petio@abv.bg"

            };


            await dbContext.AddAsync(user);

            var Camera = new CameraShop.Infrastructure.Data.Models.Camera
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
            await dbContext.AddAsync(Camera);

            var review = new AddReviewViewModel
            {
                ApplicationUserId = user.Id,
                Comment = "i like it",
                Rating = 4,
                CameraId = Camera.Id,


            };


            await reviewService.AddReview(review);


            var result  = reviewService.GetAllReview(Camera.Id);

            Assert.NotNull(result);
            Assert.AreEqual(Camera.Id, result.Id);

        }

        [Test]
        public async Task ShouldDeleteReviewById()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TechRentingDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new TechRentingDbContext(optionsBuilder.Options);

            var reviewService = new ReviewRepository(dbContext);


            var user = new CameraShop.Infrastructure.Data.Models.Account.ApplicationUser
            {
                Id = "dasod8iaijoudaij9d-1231",
                FirstName = "Petio",
                LastName = "Petrov",
                Email = "Petio@abv.bg"

            };


            await dbContext.AddAsync(user);

            var Camera = new CameraShop.Infrastructure.Data.Models.Camera
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
            await dbContext.AddAsync(Camera);

            var review = new AddReviewViewModel
            {
                ApplicationUserId = user.Id,
                Comment = "i like it",
                Rating = 4,
                CameraId = Camera.Id,


            };


            await reviewService.AddReview(review);


            var result = reviewService.DeleteReview(Camera.Id);

            Assert.NotNull(result);
         

        }
        [Test]
        public async Task UpdateReview()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TechRentingDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new TechRentingDbContext(optionsBuilder.Options);

            var reviewService = new ReviewRepository(dbContext);


            var user = new CameraShop.Infrastructure.Data.Models.Account.ApplicationUser
            {
                Id = "dasod8iaijoudaij9d-1231",
                FirstName = "Petio",
                LastName = "Petrov",
                Email = "Petio@abv.bg"

            };


            await dbContext.AddAsync(user);

            var Camera = new CameraShop.Infrastructure.Data.Models.Camera
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
            await dbContext.AddAsync(Camera);

            var review = new AddReviewViewModel
            {
                ApplicationUserId = user.Id,
                Comment = "i like it",
                Rating = 4,
                CameraId = Camera.Id,


            };


            await reviewService.AddReview(review);

            var editReview = new EditReviewViewModel()
            {
                Id = review.CameraId,
                Comment = review.Comment,
                Rating = review.Rating,
                LastUpdate = DateTime.Now,
            };
             

            var result = reviewService.Update(editReview);

            Assert.NotNull(result);
            Assert.AreEqual(editReview.Id, result.Id);
         
            

        }
    }
}
