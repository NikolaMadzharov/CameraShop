using TechRentingSystem.Data.Models.Account;
using TechRentingSystem.repository;

namespace TechRentingSystem.Repository.Repository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUserById(string id);
    }
}
