using CameraShop.Infrastructure.Data.Models;

namespace CameraShop.Core.Models.Cart
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCart> ListCart { get; set; }


        public OrderHeader OrderHeader { get; set; }
    }
}
