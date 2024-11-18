using System;
using System.Collections.Generic;

namespace demo1112remake.Models;

public partial class OrderProduct
{
    public int IdOrder { get; set; }

    public int IdProduct { get; set; }

    public int Quantity { get; set; }

    public virtual Order IdOrderNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;
}
