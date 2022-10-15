namespace Cryptocop.Software.API.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly CrytoDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public AddressController(CrytoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        // TODO: Get all addresses associated with authenticated user
        [HttpGet]
        public IActionResult GetAddresses()
        {
            throw new NotImplementedException();
        }
        
        // TODO: Adds a new address associated with authenticated user, see models section for reference. Review FromBody
        [HttpPost]
        public IActionResult AddAddress([FromBody] string address)
        {
            throw new NotImplementedException();
        }
        
        // TODO: Removes by ID, an address associated with authenticated user
        [HttpDelete("{id}")]
        public IActionResult RemoveAddress(int id)
        {
            throw new NotImplementedException();
        }
    }
}