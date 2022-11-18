using TechRentingSystem.Data;
using TechRentingSystem.Data.Models.Account;
using TechRentingSystem.Repository.IRepository;

namespace TechRentingSystem.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly TechRentingDbContext _data;

        public ApplicationUserRepository(TechRentingDbContext data) : base(data)
        {
            _data = data;
        }
    }
}
