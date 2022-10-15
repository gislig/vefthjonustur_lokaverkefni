namespace Cryptocop.Software.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly CrytoDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public AccountController(CrytoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        // TODO: Register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterInputModel userRegistrationInput)
        {
            throw new NotImplementedException();
        }
        
        // TODO: Signin
        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] LoginInputModel loginInput)
        {
            throw new NotImplementedException();
        }
        
        // TODO: Signout
        [HttpGet("signout")]
        public async Task<IActionResult> Signout()
        {
            throw new NotImplementedException();
        }

    }
}