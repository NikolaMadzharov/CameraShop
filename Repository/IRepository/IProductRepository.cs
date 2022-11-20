using TechRentingSystem.Data.Models;
using TechRentingSystem.repository;

namespace TechRentingSystem.Repository.IRepository
{
    public interface IProductRepository :IRepository<Camera>
    {
        void Update(Camera product);
    }
}
