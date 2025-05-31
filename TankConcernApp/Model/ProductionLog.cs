using System;
using System.Collections.Generic;

namespace TankConcernApp.Model;

public partial class ProductionLog
{
    public long LogId { get; set; }

    public long WorkshopId { get; set; }

    public long BrigadeId { get; set; }

    public long OrderId { get; set; }

    public long ProductStageId { get; set; }

    public long ProductStageTypeId { get; set; }

    public DateOnly Date { get; set; }

    public string? Description { get; set; }

    public virtual BrigadeWorkshopAssignment BrigadeWorkshopAssignment { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual ProductStage ProductStage { get; set; } = null!;

    public virtual ProductStageType ProductStageType { get; set; } = null!;
}
