namespace Cryptocop.Software.API.Services.Implementations
{
    public class CryptoCurrencyService : ICryptoCurrencyService
    {
        private readonly IMessariResolverService _messariResolverService;
        
        public CryptoCurrencyService(IMessariResolverService messariResolverService)
        {
            _messariResolverService = messariResolverService;
        }
        
        public Task<IEnumerable<CryptoCurrencyDto>> GetAvailableCryptocurrencies()
        {
            return _messariResolverService.GetAllCryptoAssets();
        }
    }
}