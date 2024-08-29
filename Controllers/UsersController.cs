using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrangeCoreApiTasks.Models;

namespace OrangeCoreApiTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private MyDbContext _db;

        public UsersController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _db.Users;
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var users = _db.Users;
            return Ok(users);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName == name);
            if (user == null) return NotFound();
            return Ok(User);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var user = _db.Users.Find();
            if (user == null) return NotFound();
            _db.Users.Remove(user);
            _db.SaveChanges();
            return NoContent();
        }


    }
}
