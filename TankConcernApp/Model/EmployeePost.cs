namespace TankConcernApp.Model;

public partial class EmployeePost
{
    public long EmployeePostId { get; set; }

    public string EmployeePostName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
