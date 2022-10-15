namespace Cryptocop.Software.API.Repositories.Entities;

public class JwtToken
{
    [Key]
    public int Id { get; set; }
    bool Blacklisted { get; set; }
}