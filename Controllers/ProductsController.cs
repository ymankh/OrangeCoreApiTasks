using Microsoft.AspNetCore.Mvc;
using OrangeCoreApiTasks.DTOs;
using OrangeCoreApiTasks.Models;

namespace OrangeCoreApiTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(MyDbContext context, IWebHostEnvironment env) : ControllerBase
    {
        private readonly IWebHostEnvironment _env = env;

        [HttpGet]
        public IActionResult Get(string? sortField, string? sortOrder)
        {
            var products = context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(sortField) && sortField == "price")
                products = products.OrderBy(p => p.Price);
            return Ok(products.ToList());
        }

        [HttpPost]
        public IActionResult Create([FromForm] ProductDto product)
        {
            Product newProduct = product;
            context.Products.Add(newProduct);
            context.SaveChanges();

            return Ok(newProduct);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromForm] ProductDto product)
        {
            if (!context.Products.Any(p => p.ProductId == id))
                return NotFound();

            Product updatedProduct = product;
            updatedProduct.ProductId = id;

            context.Products.Update(updatedProduct);
            context.SaveChanges();

            return Ok(updatedProduct);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var product = context.Products.Find(id);
            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var product = context.Products.FirstOrDefault(p => p.ProductName == name);
            if (product == null)
                return NotFound($"couldn't find a product with the name {name}");
            if (product.ProductId > 20) return BadRequest("How dare you ask for a product with an id higher than 10");
            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product == null) return NotFound();
            context.Products.Remove(product);
            context.SaveChanges();
            return NoContent();
        }


        private bool ProductExists(int id)
        {
            return context.Products.Any(e => e.ProductId == id);
        }
    }
}
