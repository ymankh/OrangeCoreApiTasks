namespace OrangeCoreApiTasks.DTOs
{
    public class CategoryDto
    {
        public string CategoryName { get; set; } = null!;

        public IFormFile? CategoryImage { get; set; }
    }
}
