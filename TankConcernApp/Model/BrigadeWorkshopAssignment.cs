namespace TankConcernApp.Model;

public partial class BrigadeWorkshopAssignment
{
    public long WorkshopId { get; set; }

    public long BrigadeId { get; set; }

    public DateOnly AssignmentDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual EmployeeBrigade Brigade { get; set; } = null!;

    public virtual ICollection<ProductionLog> ProductionLogs { get; set; } = new List<ProductionLog>();

    public virtual Workshop Workshop { get; set; } = null!;
}
