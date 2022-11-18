using TechRentingSystem.Data;
using TechRentingSystem.Data.Models;
using TechRentingSystem.Repository.IRepository;

namespace TechRentingSystem.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private TechRentingDbContext _data;

        public ShoppingCartRepository(TechRentingDbContext data):base(data)
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
