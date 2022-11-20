using TechRentingSystem.Data.Models;

namespace TechRentingSystem.Models.Cart
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCart> ListCart { get; set; }


        public OrderHeader OrderHeader { get; set; }
    }
}
