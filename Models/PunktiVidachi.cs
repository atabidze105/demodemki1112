using System;
using System.Collections.Generic;

namespace demo1112remake.Models;

public partial class PunktiVidachi
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
