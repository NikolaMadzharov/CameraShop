using TechRentingSystem.Data.Models;
using TechRentingSystem.repository;

namespace TechRentingSystem.Repository.Repository
{
    public interface IOrderDetailRepository:IRepository<OrderDetails>
    {
        void Update(OrderDetails obj);
    }
}
