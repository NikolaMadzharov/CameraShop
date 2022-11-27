using CameraShop.Core.Models.Account;
using CameraShop.Core.Repository.IRepository;
using CameraShop.Infrastructure.Data;
using CameraShop.Infrastructure.Data.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace CameraShop.Core.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly TechRentingDbContext _data;

        public ApplicationUserRepository(TechRentingDbContext data) : base(data)
        {
            _data = data;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await _data.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserEditViewModel> GetUserForEdit(string id)
        {

            var user = await GetUserById(id);

            return new UserEditViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email

            };
        }

        public async Task<UserProfileViewModel> GetUserProfile(string id)
        {
            var user = await GetUserById(id);


            return new UserProfileViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            var users = await this._data.Users
               .Select(u => new UserListViewModel()
               {
                   Email = u.Email,
                   Id = u.Id,
                   FullName = $"{u.FirstName} {u.LastName}",
                   Username = u.UserName
               })
               .ToListAsync();

            return users;
        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            bool result = false;
            var user = await GetUserById(model.Id);


            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                await this._data.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
}

