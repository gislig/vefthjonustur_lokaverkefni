namespace Cryptocop.Software.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly CrytoDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public OrderController(CrytoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        // TODO: Get all orders associated with the authenticated user
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            throw new NotImplementedException();
        }
        
        // TODO: Add a new order associated with the authenticated ser, see Models section for reference. Remember to update FRomBody.
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderInputModel orderInput)
        {
            throw new NotImplementedException();
        }
    }
}