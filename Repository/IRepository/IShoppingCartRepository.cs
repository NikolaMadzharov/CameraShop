using TechRentingSystem.Data.Models;
using TechRentingSystem.repository;

namespace TechRentingSystem.Repository.Repository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart obj);

        int IncrementCount(ShoppingCart shoppingCart, int count);

        int DecrementCount(ShoppingCart shoppingCart, int count);
    }
}
