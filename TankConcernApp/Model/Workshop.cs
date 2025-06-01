namespace TankConcernApp.Model;

public partial class Workshop
{
    public long WorkshopId { get; set; }

    public long WorkshopTypeId { get; set; }

    public string Location { get; set; } = null!;

    public int WorkshopArea { get; set; }

    public int Workplaces { get; set; }

    public int ProductionCapacity { get; set; }

    public virtual ICollection<BrigadeWorkshopAssignment> BrigadeWorkshopAssignments { get; set; } = new List<BrigadeWorkshopAssignment>();

    public virtual ICollection<ProductStage> ProductStages { get; set; } = new List<ProductStage>();

    public virtual WorkshopType WorkshopType { get; set; } = null!;
}
