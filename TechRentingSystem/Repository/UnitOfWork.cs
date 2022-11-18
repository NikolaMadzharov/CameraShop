using TechRentingSystem.Data;
using TechRentingSystem.Data.Models;
using TechRentingSystem.Repository.IRepository;

namespace TechRentingSystem.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TechRentingDbContext _data;

        public UnitOfWork(TechRentingDbContext data)
        {
          _data = data;
           ApplicationUser = new ApplicationUserRepository(_data);
           ShoppingCart = new ShoppingCartRepository(_data);
            Product = new ProductRepositry(_data);
        }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IProductRepository Product { get; private set; }

        public void Dispose()
        {
            _data.Dispose();
        }

        public void Save()
        {
            _data.SaveChanges();
        }
    }
    
}
