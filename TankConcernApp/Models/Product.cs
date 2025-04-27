using System;
using System.Collections.Generic;

namespace TankConcernApp.Models;

public partial class Product
{
    public long ProductId { get; set; }

    public long ProductCategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ProductCategory ProductCategory { get; set; } = null!;

    public virtual ICollection<TankPart> TankParts { get; set; } = new List<TankPart>();
}
