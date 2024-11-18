using System;
using System.Collections.Generic;

namespace demo1112remake.Models;

public partial class Satu
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
