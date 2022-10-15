namespace Cryptocop.Software.API.Repositories.Entities;

public class OrderItem
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public Order Order { get; set; }
    
    string ProductIdentifier { get; set; }
    double? Quantity { get; set; }
    double? UnitPrice { get; set; }
    double? TotalPrice { get; set; }
}