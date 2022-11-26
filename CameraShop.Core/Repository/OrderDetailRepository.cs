using CameraShop.Core.Repository.IRepository;
using CameraShop.Infrastructure.Data;
using CameraShop.Infrastructure.Data.Models;

namespace CameraShop.Core.Repository
{
    public class OrderDetailRepository : Repository<OrderDetails>, IOrderDetailRepository
    {
        private TechRentingDbContext _data;

        public OrderDetailRepository(TechRentingDbContext data) : base(data)
        {
            _data = data;
        }

        public void Update(OrderDetails obj)
        {
            _data.OrderDetails.Update(obj);
        }
    }
}
