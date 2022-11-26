using CameraShop.Core.Repository.IRepository;
using CameraShop.Infrastructure.Data;
using CameraShop.Infrastructure.Data.Models;

namespace CameraShop.Core.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private TechRentingDbContext _data;

        public ShoppingCartRepository(TechRentingDbContext data) : base(data)
        {
            _data = data;
        }

        public int DecrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
            return shoppingCart.Count;
        }

        public int IncrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            return shoppingCart.Count;
        }

        public void Update(ShoppingCart obj)
        {
            _data.Update(obj);
        }
    }
}
