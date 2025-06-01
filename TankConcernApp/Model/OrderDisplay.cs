namespace TankConcernApp.Model
{
    public class OrderDisplay
    {
        public long OrderId { get; set; }
        public string CustomerName { get; set; }
        public DateOnly OrderDate { get; set; }
        public string ProductName { get; set; }
        public string OrderStatusName { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
