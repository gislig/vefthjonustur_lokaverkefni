namespace Cryptocop.Software.API.Repositories.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string HashedPassword { get; set; } = "";
    
    IEnumerable<PaymentCard> PaymentCards { get; set; }
    IEnumerable<Address> Addresses { get; set; }
    IEnumerable<Order> Orders { get; set; }
}