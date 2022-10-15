namespace Cryptocop.Software.API.Repositories.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }    
    
    string Email { get; set; }
    string FullName { get; set; }
    string StreetName { get; set; }
    string HouseNumber { get; set; }
    string ZipCode { get; set; }
    string City { get; set; }
    string Country { get; set; }
    string CardHolderName { get; set; }
    string MaskedCreditCard { get; set; }
    DateTime OrderDate { get; set; }
    double? TotalPrice { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; }
}