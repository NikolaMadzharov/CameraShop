namespace CameraShop.Infrastructure.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Camera> Cameras { get; set; } = new List<Camera>();
    }
}
