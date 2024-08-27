using Microsoft.AspNetCore.Mvc;
using OrangeCoreApiTasks.DTOs;
using OrangeCoreApiTasks.Models;
using static OrangeCoreApiTasks.Shared.Shared;

namespace OrangeCoreApiTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(MyDbContext context) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(context.Categories.ToList());
        }

        [HttpPost]
        public IActionResult Create(CategoryDto category)
        {
            var newCategory = new Category
            {
                CategoryName = category.CategoryName,
                CategoryImage = SaveImage(category.CategoryImage)
            };
            context.Categories.Add(newCategory);
            return Ok(newCategory);
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, CategoryDto category)
        {
            var oldCategory = context.Categories.Find(id);

            if (oldCategory == null)
                return NotFound();
            oldCategory.CategoryName = category.CategoryName;
            oldCategory.CategoryImage = SaveImage(category.CategoryImage);

            context.Categories.Update(oldCategory);
            return Ok(oldCategory);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var category = context.Categories.Find(id);
            if (category == null) return NotFound($"We couldn't find a category with the id {id}");
            return Ok(category);
        }

        [HttpGet("{id:int}/products")]
        public IActionResult GetProducts(int id)
        {
            var category = context.Categories.Find(id);
            if (category == null) return NotFound($"We couldn't find a category with the id {id}");

            return Ok(context.Products.Where(p => p.CategoryId == id).ToList());
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var category = context.Categories.FirstOrDefault(c => c.CategoryName == name);
            if (category == null) return NotFound($"We couldn't find a category with the name {name}");
            return Ok(category);
        }



    }
}
