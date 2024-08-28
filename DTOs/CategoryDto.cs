using OrangeCoreApiTasks.Models;
using static OrangeCoreApiTasks.Shared.Shared;

namespace OrangeCoreApiTasks.DTOs
{
    public class CategoryDto
    {
        public string CategoryName { get; set; } = null!;

        public IFormFile? CategoryImage { get; set; }

        public static implicit operator Category(CategoryDto category)
        {
            return new Category
            {
                CategoryName = category.CategoryName,
                CategoryImage = SaveImage(category.CategoryImage)
            };
        }
    }
}
