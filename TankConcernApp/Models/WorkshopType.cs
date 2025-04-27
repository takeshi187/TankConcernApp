using System;
using System.Collections.Generic;

namespace TankConcernApp.Models;

public partial class WorkshopType
{
    public long WorkshopTypeId { get; set; }

    public string WorkshopTypeName { get; set; } = null!;

    public virtual ICollection<Workshop> Workshops { get; set; } = new List<Workshop>();
}
