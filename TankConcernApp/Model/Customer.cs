using System;
using System.Collections.Generic;

namespace TankConcernApp.Model;

public partial class Customer
{
    public long CustomerId { get; set; }

    public long CustomerTypeId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Description { get; set; }

    public virtual CustomerType CustomerType { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
