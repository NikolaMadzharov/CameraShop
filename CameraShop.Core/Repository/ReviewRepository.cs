using Microsoft.EntityFrameworkCore;
using CameraShop.Core.Models.Review;
using CameraShop.Core.Repository.IRepository;
using CameraShop.Infrastructure.Data.Models;
using CameraShop.Infrastructure.Data;

namespace CameraShop.Core.Repository
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
            var user = await _data.Users.FirstOrDefaultAsync(x => x.Id == model.ApplicationUserId);

            if (user == null)
            {
                throw new Exception();
            }

            var course = await _data.Cameras.FirstOrDefaultAsync(x => x.Id == model.CameraId);

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


            await _data.AddAsync(review);
            await _data.SaveChangesAsync();
        }

        public async Task DeleteReview(int id)
        {
            var review = await _data.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if (review == null)
            {
                throw new Exception();
            }


            await _data.SaveChangesAsync();
        }

        public async Task<IEnumerable<Review>> GetAllReview(int cameraId)
        {
            var allReviews = await _data.Reviews.Where(x => x.CameraId == cameraId)
                .Include(x => x.ApplicationUser)
                .ToListAsync();

            return allReviews;
        }

        public Task<EditReviewViewModel> GetReviewForEdit(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(EditReviewViewModel model)
        {
            var review = await _data.Reviews.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (review == null)
            {
                throw new Exception();
            }

            review.Rating = model.Rating;
            review.Comment = model.Comment;
            review.LastUpdate = model.LastUpdate;

            await _data.SaveChangesAsync();
        }
    }
}
