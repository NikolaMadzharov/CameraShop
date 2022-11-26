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
    }
}
