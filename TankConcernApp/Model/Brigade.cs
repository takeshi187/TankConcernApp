﻿namespace TankConcernApp.Model;

public partial class Brigade
{
    public long BrigadeId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<EmployeeBrigade> EmployeeBrigades { get; set; } = new List<EmployeeBrigade>();
}
