namespace Cryptocop.Software.API.Repositories.Entities;

public class ShoppingCart
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
}