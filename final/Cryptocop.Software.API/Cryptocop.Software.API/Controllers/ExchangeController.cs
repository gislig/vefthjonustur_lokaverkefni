namespace Cryptocop.Software.API.Controllers
{
    [Route("api/exchanges")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly CrytoDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public ExchangeController(CrytoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        // TODO: Get All exchanges in a paginated envelope
        [HttpGet]
        public async Task<IActionResult> GetExchanges([FromQuery] int paginationQuery)
        {
            throw new NotImplementedException();
        }
    }
}