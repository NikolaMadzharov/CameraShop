using TechRentingSystem.Data;
using TechRentingSystem.Data.Models;
using TechRentingSystem.Repository.Repository;

namespace TechRentingSystem.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private TechRentingDbContext _data;

        public OrderHeaderRepository(  TechRentingDbContext data):base(data)
        {
            _data = data;
        }

        public void Update(OrderHeader obg)
        {
           _data.OrderHeaders.Update(obg);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
           var orderFromDb = _data.OrderHeaders.FirstOrDefault(x => x.Id == id);

            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
               
            }
        }

        public void UpdateStripePaymendId(int id, string sessionId, string? paymentItentId )
        {
            var orderFromDb = _data.OrderHeaders.FirstOrDefault(x => x.Id == id);

            orderFromDb.SessionId = sessionId;
            orderFromDb.PaymentIntendId = paymentItentId;
       }
    }
}
