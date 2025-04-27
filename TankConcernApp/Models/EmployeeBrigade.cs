using System;
using System.Collections.Generic;

namespace TankConcernApp.Models;

public partial class EmployeeBrigade
{
    public long Ebid { get; set; }

    public long BrigadeId { get; set; }

    public long EmployeeId { get; set; }

    public DateOnly LastUpdate { get; set; }

    public long OldBrigadeId { get; set; }

    public virtual Brigade Brigade { get; set; } = null!;

    public virtual ICollection<BrigadeWorkshopAssignment> BrigadeWorkshopAssignments { get; set; } = new List<BrigadeWorkshopAssignment>();

    public virtual Employee Employee { get; set; } = null!;
}
