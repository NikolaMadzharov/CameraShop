using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using TechRentingSystem.Contracts;
using TechRentingSystem.Data;
using TechRentingSystem.Models.Product;

namespace TechRentingSystem.Services
{
    public class ProductService : IProductDetails
    {
        private readonly TechRentingDbContext context;

        public ProductService(TechRentingDbContext _context)
        {
           context = _context;
        }

        public async Task<ProductDetailsModel> GetProductDetails(int productId)
        {
            var product = await context.Cameras.FirstOrDefaultAsync(x => x.Id == productId);
            var model = new ProductDetailsModel()
            {
                Id = productId,
                Brand = product.Brand,
                Model = product.Model,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Count = 1
            };
        

            return model;

        }
    }
}
