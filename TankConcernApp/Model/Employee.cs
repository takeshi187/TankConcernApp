using System;
using System.Collections.Generic;

namespace TankConcernApp.Model;

public partial class Employee
{
    public long EmployeeId { get; set; }

    public long EmployeePostId { get; set; }

    public long EmployeeStatusId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public decimal Salary { get; set; }

    public decimal Phone { get; set; }

    public virtual EmployeeBrigade? EmployeeBrigade { get; set; }

    public virtual EmployeePost EmployeePost { get; set; } = null!;

    public virtual EmployeeStatus EmployeeStatus { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
