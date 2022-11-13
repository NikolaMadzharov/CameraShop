using TechRentingSystem.Models.Product;

namespace TechRentingSystem.Contracts
{
    public interface IProductDetails
    {
        Task<ProductDetailsModel> GetProductDetails(int productId);
    }
}
