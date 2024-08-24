using System;
using System.Collections.Generic;

namespace OrangeCoreApiTasks.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
