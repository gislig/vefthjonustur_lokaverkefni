namespace Cryptocop.Software.API.Repositories.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }    
    
    public string Email { get; set; }
    public string FullName { get; set; }
    public string StreetName { get; set; }
    public string HouseNumber { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string CardHolderName { get; set; }
    public string MaskedCreditCard { get; set; }
    public DateTime OrderDate { get; set; }
    public double? TotalPrice { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; }
}