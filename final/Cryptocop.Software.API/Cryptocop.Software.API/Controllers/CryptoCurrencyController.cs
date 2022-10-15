namespace Cryptocop.Software.API.Controllers
{
    [ApiController]
    [Route("api/cryptocurrencies")]
    public class CryptoCurrencyController : ControllerBase
    {
        private readonly CrytoDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public CryptoCurrencyController(CrytoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        // TODO: Get all available cryptocurrencies BitCoin (BTC), Ethereum (ETH), Tether (USDT) and Monero (XMR)
        [HttpGet]
        public async Task<IActionResult> GetCryptocurrencies()
        {
            throw new NotImplementedException();
        }
    }
}
