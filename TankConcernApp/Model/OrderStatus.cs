using System;
using System.Collections.Generic;

namespace TankConcernApp.Model;

public partial class OrderStatus
{
    public long OrderStatusId { get; set; }

    public string OrderStatusName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
