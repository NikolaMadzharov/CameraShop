using CameraShop.Core.Models.Home;
using CameraShop.Core.Models.Product;
using CameraShop.Core.Models.Review;
using CameraShop.Infrastructure.Data.Models;

namespace CameraShop.Core.Repository.IRepository
{
    public interface IProductRepository : IRepository<Camera>
    {
        void Update(Camera product);

        public Task<IEnumerable<CameraIndexViewModel>> GetCamerasAsync();


        public Task<IEnumerable<CameraCategoryViewModel>> GetCameraCategories();

        Task Delete(int id);

        public Task Add(AddCameraFromModel model);
        

    }
}
