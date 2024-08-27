using OrangeCoreApiTasks.Models;

namespace OrangeCoreApiTasks.DTOs
{
    public class ProductDto
    {
        public string ProductName { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public IFormFile? ProductImage { get; set; }
    }
}
