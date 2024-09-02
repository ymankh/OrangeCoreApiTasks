using System.ComponentModel.DataAnnotations;
using OrangeCoreApiTasks.Models;
using static OrangeCoreApiTasks.Shared.PasswordHasher;

namespace OrangeCoreApiTasks.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(4)]
        [MaxLength(255)]
        public string Password { get; set; } = string.Empty;

        public User Create(MyDbContext context)
        {
            var (pass, salt) = CreatePasswordHash(Password);
            var user = new User
            {
                UserName = this.UserName,
                Email = this.Email,
                Password = pass,
                PasswordSalt = salt
            };
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }
    }

    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(4)]
        [MaxLength(255)]
        public string Password { get; set; } = string.Empty;

        public User? AuthentecateUser(MyDbContext context)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == Email);
            if (user == null)
                return null;


            return VerifyPasswordHash(this.Password, user.Password, user.PasswordSalt) ? user : null;
        }
    }
}
