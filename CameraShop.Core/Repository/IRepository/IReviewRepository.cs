using CameraShop.Core.Models.Review;
using CameraShop.Infrastructure.Data.Models;

namespace CameraShop.Core.Repository.IRepository
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task AddReview(AddReviewViewModel model);

        Task DeleteReview(int id);

        Task<EditReviewViewModel> GetReviewForEdit(int id);

        Task Update(EditReviewViewModel model);

        Task<IEnumerable<Review>> GetAllReview(int cameraId);
    }
}
