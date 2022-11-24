using TechRentingSystem.Data;
using TechRentingSystem.Data.Models;
using TechRentingSystem.Repository.Repository;

namespace TechRentingSystem.Repository
{
    public class OrderDetailRepository : Repository<OrderDetails>, IOrderDetailRepository
    {
        private TechRentingDbContext _data;

        public OrderDetailRepository(TechRentingDbContext data):base(data)
        {
            _data = data;
        }

        public void Update(OrderDetails obj)
        {
            _data.OrderDetails.Update(obj);
        }
    }
}
