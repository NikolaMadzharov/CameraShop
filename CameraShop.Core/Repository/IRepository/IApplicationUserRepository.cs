using CameraShop.Infrastructure.Data.Models.Account;

namespace CameraShop.Core.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUserById(string id);
    }
}
