using System;
using System.Collections.Generic;

namespace TankConcernApp.Model;

public partial class ProductStageType
{
    public long ProductStageTypeId { get; set; }

    public string ProductStageTypeName { get; set; } = null!;

    public virtual ICollection<ProductStage> ProductStages { get; set; } = new List<ProductStage>();

    public virtual ICollection<ProductionLog> ProductionLogs { get; set; } = new List<ProductionLog>();
}
