using Microsoft.EntityFrameworkCore;
using TechRentingSystem.Data;
using TechRentingSystem.Data.Models;
using TechRentingSystem.Models.Review;
using TechRentingSystem.Models.Home;
using TechRentingSystem.Repository.Repository;
using TechRentingSystem.Data.Models.Account;

namespace TechRentingSystem.Repository
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly TechRentingDbContext _data;

        public ReviewRepository(TechRentingDbContext data) : base(data)
        {
            _data = data;
        }

        public async Task AddReview(AddReviewViewModel model)
        {
            var user = await this._data.Users.FirstOrDefaultAsync(x => x.Id == model.ApplicationUserId);

            if (user == null)
            {
                throw new Exception();
            }

            var course = await this._data.Cameras.FirstOrDefaultAsync(x => x.Id == model.CameraId);

            if (course == null)
            {
                throw new Exception();
            }

            var review = new Review()
            {
                Comment = model.Comment,
                Rating = model.Rating,
                CameraId = model.CameraId,
                ApplicationUserId = model.ApplicationUserId,
                DateOfPublication = model.DateOfPublication,
            };

            await this._data.AddAsync(review);
            await this._data.SaveChangesAsync();
        }

        public async Task DeleteReview(int id)
        {
            var review = await this._data.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if (review == null)
            {
                throw new Exception();
            }


            await this._data.SaveChangesAsync();
        }

        public Task<EditReviewViewModel> GetReviewForEdit(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(EditReviewViewModel model)
        {
            var review = await this._data.Reviews.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (review == null)
            {
                throw new Exception();
            }

            review.Rating = model.Rating;
            review.Comment = model.Comment;
            review.LastUpdate = model.LastUpdate;

            await this._data.SaveChangesAsync();
        }
    }
}
