using CameraShop.Infrastructure.Data.Models;

namespace CameraShop.Core.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader obg);

        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
        public void UpdateStripePaymendId(int id, string sessionId, string? paymentItentId);
    }
}
