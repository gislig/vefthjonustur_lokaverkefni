namespace Cryptocop.Software.API.Repositories.Entities;

public class PaymentCard
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }

    public string CardholderName { get; set; } = "";
    public string CardNumber { get; set; } = "";
    public int Month { get; set; }
    public int Year { get; set; }
}