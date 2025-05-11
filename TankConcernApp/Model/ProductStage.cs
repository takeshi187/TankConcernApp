using System;
using System.Collections.Generic;

namespace TankConcernApp.Model;

public partial class ProductStage
{
    public long ProductStageId { get; set; }

    public long ProductStageTypeId { get; set; }

    public long WorkshopId { get; set; }

    public long OrderId { get; set; }

    public string? Description { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ProductStageType ProductStageType { get; set; } = null!;

    public virtual ICollection<ProductionLog> ProductionLogs { get; set; } = new List<ProductionLog>();

    public virtual Workshop Workshop { get; set; } = null!;
}
