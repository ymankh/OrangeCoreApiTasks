using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OrangeCoreApiTasks.Models;

public partial class User 
{

    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[]? Password { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
