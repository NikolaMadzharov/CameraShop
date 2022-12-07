namespace CameraShop.Infrastructure.Data.Models
{
    public class Camera
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public int Year { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public bool IsActive { get; set; }

        public Category Category { get; set; }

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
