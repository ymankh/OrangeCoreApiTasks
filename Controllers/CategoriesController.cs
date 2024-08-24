using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrangeCoreApiTasks.Models;

namespace OrangeCoreApiTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CategoriesController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Categories.ToList());
        }
        [HttpGet("{id:int:min(5)}")]
        public IActionResult Get(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound($"We couldn't find a category with the id {id}");
            return Ok(category);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryName == name);
            if (category == null) return NotFound($"We couldn't find a category with the name {name}");
            return Ok(category);
        }

    }
}
