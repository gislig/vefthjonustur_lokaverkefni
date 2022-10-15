namespace Cryptocop.Software.API.Repositories.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    
    string FullName { get; set; } = "";
    string Email { get; set; } = "";
    string HashedPassword { get; set; } = "";
    
    IEnumerable<PaymentCard> PaymentCards { get; set; }
    IEnumerable<Address> Addresses { get; set; }
    IEnumerable<Order> Orders { get; set; }
}