using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrangeCoreApiTasks.DTOs;
using OrangeCoreApiTasks.Shared;
using System.Data;

namespace OrangeCoreApiTasks.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(MyDbContext context, TokenGenerator tokenGenerator) : ControllerBase
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
                return Unauthorized();
            var token = tokenGenerator.GenerateToken(user.UserName, new List<string>());

            return Ok(new { Token = token });
        }

        [HttpPost("getUserByUsername")]
        public IActionResult GetByUserName([FromForm] string userName)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                return NotFound($"No user with the username {userName}");
            }
            return Ok(user);
        }
    }
}
