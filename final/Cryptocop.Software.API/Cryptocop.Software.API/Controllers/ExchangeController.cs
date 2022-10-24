namespace Cryptocop.Software.API.Controllers
{
    [Route("api/exchanges"), Authorize]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeService _exchangeService;
        
        public ExchangeController(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }
        
        [HttpGet]
        public async Task<Envelope<ExchangeDto>> GetExchanges([FromQuery] int paginationQuery = 1)
        {
            return await _exchangeService.GetExchanges(paginationQuery);
        }
    }
}