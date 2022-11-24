using Microsoft.EntityFrameworkCore;
using TechRentingSystem.Data;
using TechRentingSystem.Data.Models;
using TechRentingSystem.Models.Cameras;
using TechRentingSystem.Models.Home;
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

        public async Task Add(AddCameraFromModel model)
        {
            var cameraData = new Camera
            {
                Brand = model.Brand,
                Model = model.Model,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Year = model.Year,
                Price = model.Price,
                CategoryId = model.CategoryId
            };
            await this._data.AddAsync(cameraData);
            await this._data.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var camera = await _data.Cameras.FirstOrDefaultAsync(x => x.Id == id);

            if (camera == null)
            {
                throw new ArgumentException("The camera is not found!");
            }

            _data.Remove(camera);
            await _data.SaveChangesAsync();
        }

        public async Task<IEnumerable<CameraCategoryViewModel>> GetCameraCategories()
        {
           var cameraCategories =  await this._data.Categories.
                Select(
                x => new CameraCategoryViewModel 
                {
                    Id = x.Id, Name = x.Name, 
                }).ToListAsync();

            return cameraCategories;
        }

        public async Task<IEnumerable<CameraIndexViewModel>> GetCamerasAsync()
        {

            var allCameras = await this._data.Cameras
                .Select(x => new CameraIndexViewModel
                {
                    Id = x.Id,
                    Brand = x.Brand,
                    Model = x.Model,
                    ImageUrl = x.ImageUrl,
                    Year = x.Year
                }).ToListAsync();

            return allCameras;
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
