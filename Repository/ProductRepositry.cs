using TechRentingSystem.Data;
using TechRentingSystem.Data.Models;
using TechRentingSystem.Repository.IRepository;

namespace TechRentingSystem.Repository
{
    public class ProductRepositry : Repository<Camera>, IProductRepository
    {
        private readonly TechRentingDbContext _data;

        public ProductRepositry(TechRentingDbContext data) : base(data)
        {
            _data = data;
        }
        public void Update(Camera product)
        {
            var objFromDb = _data.Cameras.FirstOrDefault(s => s.Id == product.Id);
            if (objFromDb != null)
            {
                if (product.ImageUrl != null)
                {
                    objFromDb.ImageUrl = product.ImageUrl;
                }
                objFromDb.Brand = product.Brand;
                objFromDb.Price = product.Price;
                objFromDb.Year = product.Year;
                objFromDb.Model = product.Model;
                objFromDb.Description = product.Description;
                objFromDb.CategoryId = product.CategoryId;


            }
        }
    }
}
