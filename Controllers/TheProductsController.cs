using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrangeCoreApiTasks.Models;

namespace OrangeCoreApiTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheProductsController : ControllerBase
    {
        MyDbContext _db;
        public TheProductsController(MyDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Get(
                [FromQuery, RegularExpression(@"^\d+$", ErrorMessage = "Category ID must be a number.")] int? cId,
                [FromQuery, RegularExpression(@"^\d+$", ErrorMessage = "Price must be a number.")] string? price
            )
        {
            var x = HttpContext.Request.Query["cId"];
            var products = _db.Products.Include(p => p.Category).AsQueryable();
            if (Convert.ToInt32(cId) > 0)
            {
                products = products.Where(p => p.CategoryId == Convert.ToInt32(cId));
            }
            if (Convert.ToInt32(price) > 0)
            {
                products = products.Where(p => p.Price > Convert.ToInt32(price));
            }
            return Ok(products.Count());
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var product = _db.Products.Find(id);
            return Ok(product);
        }

        [HttpGet("name/{name}")]
        public IActionResult Get([RegularExpression(@"^[a-zA-Z]{3,50}$", ErrorMessage = "Name must be alphabetic and between 3 and 50 characters long.")] string name)
        {
            var product = _db.Products.FirstOrDefault(p => p.ProductName == name);
            if (product == null)
                return BadRequest($"Can not find a product with the name {name}");
            return Ok(product);
        }
    }
}
