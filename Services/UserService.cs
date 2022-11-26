using CameraShop.Core.Models.Account;
using CameraShop.Infrastructure.Data;
using CameraShop.Infrastructure.Data.Models.Account;
using Microsoft.EntityFrameworkCore;
using TechRentingSystem.Contracts;

namespace TechRentingSystem.Services
{
    public class UserService :IUserService
    {
        private readonly TechRentingDbContext context ;

        public UserService(TechRentingDbContext _context)
        {
            context = _context;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await this.context.Users.FirstOrDefaultAsync(context => context.Id == id);
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


        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            var users = await this.context.Users
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

                await this.context.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
}
