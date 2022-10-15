namespace Cryptocop.Software.API.Repositories.Contexts;

public class CrytoDbContext : DbContext
{
    public CrytoDbContext(DbContextOptions<CrytoDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<JwtToken> JwtTokens { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PaymentCard> PaymentCards { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}