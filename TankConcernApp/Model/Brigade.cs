using System;
using System.Collections.Generic;

namespace TankConcernApp.Model;

public partial class Brigade
{
    public long BrigadeId { get; set; }

    public string? Description { get; set; }

    public virtual EmployeeBrigade? EmployeeBrigade { get; set; }
}
