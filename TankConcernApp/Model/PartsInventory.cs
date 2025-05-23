﻿namespace TankConcernApp.Model;

public partial class PartsInventory
{
    public long InventoryId { get; set; }

    public long TankPartId { get; set; }

    public long Count { get; set; }

    public DateOnly LastUpdate { get; set; }

    public long PartTypeId { get; set; }

    public virtual PartType PartType { get; set; } = null!;

    public virtual TankPart TankPart { get; set; } = null!;
}
