using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrangeCoreApiTasks.DTOs;

namespace OrangeCoreApiTasks.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(MyDbContext context) : ControllerBase
    {
        [HttpPost("Register")]
        public IActionResult Register([FromForm] RegisterDto user)
        {
            var newUser = user.Create(context);
            return Ok(newUser);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromForm] LoginDto loginInfo)
        {
            var user = loginInfo.AuthentecateUser(context);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
    }
}
