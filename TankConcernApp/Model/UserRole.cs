namespace TankConcernApp.Model;

public partial class UserRole
{
    public long RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
