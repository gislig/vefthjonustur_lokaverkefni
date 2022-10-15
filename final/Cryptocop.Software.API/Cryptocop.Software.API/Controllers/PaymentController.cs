namespace Cryptocop.Software.API.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly CrytoDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public PaymentController(CrytoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        // TODO: Get all payment cards associated with the authenticated user
        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            throw new NotImplementedException();
        }
        
        // TODO: Adds a new payment card associated with the authenticated user
        [HttpPost]
        public async Task<IActionResult> AddPayment([FromBody] PaymentCardInputModel paymentInput)
        {
            throw new NotImplementedException();
        }
    }
}