using CameraShop.Core.Models.Enum;
using CameraShop.Core.Models.Home;
using CameraShop.Core.Models.Product;
using CameraShop.Core.Repository.IRepository;
using CameraShop.Infrastructure.Data;
using CameraShop.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CameraShop.Core.Repository
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
            await _data.AddAsync(cameraData);
            await _data.SaveChangesAsync();
        }

     

        public async Task Delete(int id)
        {
            var camera = await _data.Cameras.FirstOrDefaultAsync(x => x.Id == id);
            var reviewCamera = await _data.Reviews.FirstOrDefaultAsync(x => x.CameraId == id);

            if (camera == null)
            {
                throw new ArgumentException("The camera is not found!");
            }

            if (reviewCamera != null)
            {
                _data.Remove(reviewCamera);
            }

            _data.Remove(camera);
            await _data.SaveChangesAsync();
        }

        public async Task<IEnumerable<CameraCategoryViewModel>> GetCameraCategories()
        {
            var cameraCategories = await _data.Categories.
                 Select(
                 x => new CameraCategoryViewModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                 }).ToListAsync();

            return cameraCategories;
        }

        public async Task<IEnumerable<CameraIndexViewModel>> GetCamerasAsync()
        {

            var allCameras = await _data.Cameras
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

       
    }
}