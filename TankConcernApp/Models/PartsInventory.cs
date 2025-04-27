using System;
using System.Collections.Generic;

namespace TankConcernApp.Models;

public partial class PartsInventory
{
    public long InvertoryId { get; set; }

    public long PartId { get; set; }

    public long Count { get; set; }

    public DateOnly LastUpdate { get; set; }

    public long PartTypeId { get; set; }

    public virtual TankPart Part { get; set; } = null!;

    public virtual PartType PartType { get; set; } = null!;
}
