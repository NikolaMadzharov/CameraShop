using TechRentingSystem.Data.Models;
using TechRentingSystem.Models.Review;
using TechRentingSystem.repository;

namespace TechRentingSystem.Repository.Repository
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task AddReview(AddReviewViewModel model);

        Task DeleteReview(int id);

        Task<EditReviewViewModel> GetReviewForEdit(int id);

        Task Update(EditReviewViewModel model);
    }
}
