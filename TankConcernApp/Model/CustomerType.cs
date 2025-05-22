namespace TankConcernApp.Model;

public partial class CustomerType
{
    public long CustomerTypeId { get; set; }

    public string? CustomerTypeName { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
