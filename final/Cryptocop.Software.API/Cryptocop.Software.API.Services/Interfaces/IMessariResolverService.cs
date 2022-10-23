namespace Cryptocop.Software.API.Services.Interfaces;

public interface IMessariResolverService
{
    Task<IEnumerable<CryptoCurrencyDto>> GetAllCryptoAssets();
    Task<IEnumerable<ExchangeDto>> GetAllExchanges();
}