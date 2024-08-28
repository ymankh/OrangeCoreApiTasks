using OrangeCoreApiTasks.Models;
using static OrangeCoreApiTasks.Shared.Shared;


namespace OrangeCoreApiTasks.DTOs
{
    public class ProductDto
    {
        public string ProductName { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public IFormFile? ProductImage { get; set; }

        public static implicit operator Product(ProductDto product)
        {
            return new Product
            {
                ProductName = product.ProductName,
                ProductImage = SaveImage(product.ProductImage),
                CategoryId = product.CategoryId,
                Price = product.Price,
                Description = product.Description
            };
        }

    }
}
