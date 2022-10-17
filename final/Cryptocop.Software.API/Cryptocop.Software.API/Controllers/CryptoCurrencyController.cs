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
            throw new NotImplementedException();
        }
    }
}
