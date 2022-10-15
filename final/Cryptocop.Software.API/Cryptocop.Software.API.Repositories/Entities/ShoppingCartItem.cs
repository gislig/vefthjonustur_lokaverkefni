namespace Cryptocop.Software.API.Repositories.Entities;

public class ShoppingCartItem
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("ShoppingCart")]
    public int ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    
    string ProductIdentifier { get; set; }
    double? Quantity { get; set; }
    double? UnitPrice { get; set; }
}