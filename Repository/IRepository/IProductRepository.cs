using TechRentingSystem.Data.Models;
using TechRentingSystem.Models.Cameras;
using TechRentingSystem.Models.Home;
using TechRentingSystem.repository;

namespace TechRentingSystem.Repository.IRepository
{
    public interface IProductRepository :IRepository<Camera>
    {
        void Update(Camera product);

        public Task<IEnumerable<CameraIndexViewModel>> GetCamerasAsync();


        public Task<IEnumerable<CameraCategoryViewModel>> GetCameraCategories();

        Task Delete(int id);

        public Task Add(AddCameraFromModel model);

    }
}
