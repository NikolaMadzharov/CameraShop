using TechRentingSystem.Data.Models;
using TechRentingSystem.repository;

namespace TechRentingSystem.Repository.IRepository
{
    public interface IOrderDetailRepository:IRepository<OrderDetails>
    {
        void Update(OrderDetails obj);
    }
}
