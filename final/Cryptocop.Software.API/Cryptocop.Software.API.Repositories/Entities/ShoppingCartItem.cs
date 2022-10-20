namespace Cryptocop.Software.API.Repositories.Entities;

public class ShoppingCartItem
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("ShoppingCart")]
    public int ShoppingCartId { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    
    public string ProductIdentifier { get; set; }
    public float Quantity { get; set; }
    public float UnitPrice { get; set; }
}