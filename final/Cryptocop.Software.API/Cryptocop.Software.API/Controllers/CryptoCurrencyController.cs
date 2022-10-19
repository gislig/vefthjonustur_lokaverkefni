namespace Cryptocop.Software.API.Controllers
{
    [ApiController]
    [Route("api/cryptocurrencies")]
    public class CryptoCurrencyController : ControllerBase
    {
        private readonly ICryptoCurrencyService _cryptoCurrencyService;
        
        public CryptoCurrencyController(ICryptoCurrencyService cryptoCurrencyService)
        {
            _cryptoCurrencyService = cryptoCurrencyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCryptocurrencies()
        {
            try
            {
                return Ok(_cryptoCurrencyService.GetAvailableCryptocurrencies());
                
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
