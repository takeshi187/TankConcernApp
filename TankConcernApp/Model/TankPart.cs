namespace TankConcernApp.Model;

public partial class TankPart
{
    public long TankPartId { get; set; }

    public string TankPartName { get; set; } = null!;

    public long ProductId { get; set; }

    public long PartTypeId { get; set; }

    public virtual PartType PartType { get; set; } = null!;

    public virtual ICollection<PartsInventory> PartsInventories { get; set; } = new List<PartsInventory>();

    public virtual Product Product { get; set; } = null!;
}
