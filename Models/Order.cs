using System;
using System.Collections.Generic;

namespace demo1112remake.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly DateForming { get; set; }

    public DateOnly DateDelivery { get; set; }

    public int Code { get; set; }

    public int Status { get; set; }

    public int PunktVidachi { get; set; }

    public int? Client { get; set; }

    public string? ClientFio { get; set; }

    public virtual User? ClientNavigation { get; set; }

    public virtual PunktiVidachi PunktVidachiNavigation { get; set; } = null!;

    public virtual Satu StatusNavigation { get; set; } = null!;
}
