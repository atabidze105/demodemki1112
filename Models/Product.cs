using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;

namespace demo1112remake.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Articul { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Measure { get; set; } = null!;

    public float Price { get; set; }

    public float MaxDiscount { get; set; }

    public float ActualDiscount { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public int Manufacturer { get; set; }

    public int Supplier { get; set; }

    public int Cathegory { get; set; }

    public int QuantityStored { get; set; }

    public virtual Cathegory CathegoryNavigation { get; set; } = null!;

    public virtual Manufacturer ManufacturerNavigation { get; set; } = null!;

    public virtual Supplier SupplierNavigation { get; set; } = null!;

    public Bitmap ProductImage => Image != null ? new Bitmap($"Assets/{Image}") : new Bitmap("Assets/picture.png");

    public string DiscountColor => ActualDiscount > 15 ? "#7fff00" : "White";

    public float PriceDiscount => ActualDiscount > 0 ? Price - Price / 100 * ActualDiscount : Price;

    public bool IsDiscounted => ActualDiscount > 0 ? true : false;
}
