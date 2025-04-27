using System;
using System.Collections.Generic;

namespace TankConcernApp.Models;

public partial class UserRole
{
    public long RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
