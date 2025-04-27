using System;
using System.Collections.Generic;

namespace TankConcernApp.Models;

public partial class ProductCategory
{
    public long ProductCategoryId { get; set; }

    public string ProductCategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
