using CameraShop.Core.Repository.IRepository;
using CameraShop.Infrastructure.Data;

namespace CameraShop.Core.Repository
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
            OrderDetail = new OrderDetailRepository(_data);
            OrderHeader = new OrderHeaderRepository(_data);
            Review = new ReviewRepository(_data);
        }

        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IProductRepository Product { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }

        public IReviewRepository Review { get; private set; }

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
