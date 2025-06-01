using System;
using System.Collections.Generic;

namespace TankConcernApp.Model;

public partial class EmployeeStatus
{
    public long EmployeeStatusId { get; set; }

    public string EmployeeStatusName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
