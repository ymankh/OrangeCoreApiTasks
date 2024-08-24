using Microsoft.AspNetCore.Mvc;
using OrangeCoreApiTasks.Models;

namespace OrangeCoreApiTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(orders db) : ControllerBase
    {
        private orders _db = db;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Orders.ToList());
        }

        [HttpPost("{id}")]
        public IActionResult Get(int id)
        {
            var order = _db.Orders.Find(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _db.Orders.Find(id);
            if (order == null) return NotFound();
            _db.Orders.Remove(order);
            return NoContent();
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName == name);
            if (user == null) return NotFound();
            var orders = _db.Orders.Where(o => o.UserId == user.UserId);
            return Ok(orders.ToList());
        }
    }
}
