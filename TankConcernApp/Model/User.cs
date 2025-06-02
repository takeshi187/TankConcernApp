namespace TankConcernApp.Model;

public partial class User
{
    public long UserId { get; set; }

    public long EmployeeId { get; set; }

    public long RoleId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly CreatedDate { get; set; }

    public DateOnly? LastLogin { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual UserRole Role { get; set; } = null!;
}
