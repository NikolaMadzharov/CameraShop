using CameraShop.Core.Models.Account;
using CameraShop.Infrastructure.Data.Models.Account;

namespace CameraShop.Core.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUserById(string id);

        Task<IEnumerable<UserListViewModel>> GetUsers();

        Task<UserProfileViewModel> GetUserProfile(string id);

        Task<UserEditViewModel> GetUserForEdit(string id);

        Task<bool> UpdateUser(UserEditViewModel model);

    }
}
