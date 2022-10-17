namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CrytoDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;
        
        public OrderRepository(CrytoDbContext dbContext, IMapper mapper, ITokenRepository tokenRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
        }
        public IEnumerable<OrderDto> GetOrders(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            
            var orders = _dbContext.Orders.Where(o => o.Email == email).ToList();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public OrderDto CreateNewOrder(string email, OrderInputModel order)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            // TODO: Hvað á að gera hérna, er ekki alveg viss hvernig á að tengja þetta samann
            var orderEntity = _mapper.Map<Order>(order);
            orderEntity.Email = email;
            orderEntity.User = user;
            orderEntity.UserId = user.Id;
            orderEntity.OrderDate = DateTime.Now.ToUniversalTime(); 
            //_dbContext.Orders.Add(orderEntity);
            //_dbContext.SaveChanges();
            
            return _mapper.Map<OrderDto>(orderEntity);
        }
    }
}