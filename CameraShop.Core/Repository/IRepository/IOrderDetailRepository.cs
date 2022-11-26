using CameraShop.Infrastructure.Data.Models;

namespace CameraShop.Core.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetails>
    {
        void Update(OrderDetails obj);
    }
}
