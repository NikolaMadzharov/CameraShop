using TechRentingSystem.Data.Models;
using TechRentingSystem.repository;

namespace TechRentingSystem.Repository.IRepository
{
    public interface IOrderHeaderRepository:IRepository<OrderHeader>
    {
        void Update(OrderHeader obg);

        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
        public void UpdateStripePaymendId(int id, string sessionId, string? paymentItentId);
    }
}
