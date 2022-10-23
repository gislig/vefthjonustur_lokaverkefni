namespace Cryptocop.Software.API.Repositories.Entities;

public class OrderItem
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public Order Order { get; set; }
    
    public string ProductIdentifier { get; set; }
    public double? Quantity { get; set; }
    public double? UnitPrice { get; set; }
    public double? TotalPrice { get; set; }
}