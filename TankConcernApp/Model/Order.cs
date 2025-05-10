using System;
using System.Collections.Generic;

namespace TankConcernApp.Model;

public partial class Order
{
    public long OrderId { get; set; }

    public long CustomerId { get; set; }

    public DateOnly OrderDate { get; set; }

    public long OrderStatusId { get; set; }

    public long ProductId { get; set; }

    public int Count { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual OrderStatus OrderStatus { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductStage> ProductStages { get; set; } = new List<ProductStage>();

    public virtual ICollection<ProductionLog> ProductionLogs { get; set; } = new List<ProductionLog>();
}
