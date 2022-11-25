using Microsoft.EntityFrameworkCore;
using TechRentingSystem.Data;
using TechRentingSystem.Data.Models.Account;
using TechRentingSystem.Repository.Repository;

namespace TechRentingSystem.Repository
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
            return await this._data.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
