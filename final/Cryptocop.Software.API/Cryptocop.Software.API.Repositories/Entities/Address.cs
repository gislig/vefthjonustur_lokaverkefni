namespace Cryptocop.Software.API.Repositories.Entities;

public class Address
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    
    public string StreetName { get; set; }
    public string HouseNumber { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
}