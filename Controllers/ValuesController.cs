using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrangeCoreApiTasks.Models;

namespace OrangeCoreApiTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        MyDbContext _db;
        public ValuesController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categories = _db.Categories.ToList();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _db.Categories.Find(id);
            return Ok(category);
        }
    }
}
