namespace Cryptocop.Software.API.Controllers
{
    [ApiController]
    [Route("api/cryptocurrencies")]
    public class CryptoCurrencyController : ControllerBase
    {
        
        // TODO: Get all available cryptocurrencies BitCoin (BTC), Ethereum (ETH), Tether (USDT) and Monero (XMR)
        [HttpGet]
        public async Task<IActionResult> GetCryptocurrencies()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            throw new NotImplementedException();
        }
    }
}
