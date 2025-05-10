using System;
using System.Collections.Generic;
using TankConcernApp.Model;

namespace TankConcernApp;

public partial class PartType
{
    public long PartTypeId { get; set; }

    public string PartTypeName { get; set; } = null!;

    public virtual ICollection<PartsInventory> PartsInventories { get; set; } = new List<PartsInventory>();

    public virtual ICollection<TankPart> TankParts { get; set; } = new List<TankPart>();
}
