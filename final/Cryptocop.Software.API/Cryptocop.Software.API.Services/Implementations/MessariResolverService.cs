namespace Cryptocop.Software.API.Services.Helpers;
public class MessariResolverService : IMessariResolverService
{
    private readonly IMapper _mapper;

    public MessariResolverService(IMapper mapper)
    {
        _mapper = mapper;
    }

    // Get all Crypto assets from Messari API
    public async Task <IEnumerable<CryptoCurrencyDto>> GetAllCryptoAssets()
    {
        var newCrypto = new List<CryptoCurrencyDto>();
        var client = new HttpClient();
        var response = client.GetAsync(
            "https://data.messari.io/api/v2/assets?fields=id,symbol,name,slug,metrics/market_data/price_usd,profile/general/overview/project_details")
            .Result;
        var Crypto = await HttpResponseMessageExtensions
            .DeserializeJsonToList<CryptoResponseDto>(response, true);

        
        _mapper.Map(Crypto, newCrypto);

        return newCrypto.Where(x => 
            x.Symbol.Equals("BTC") || 
            x.Symbol.Equals("ETH") || 
            x.Symbol.Equals("XRP") || 
            x.Symbol.Equals("USDT")
            );
    }
    
    // Get all Exchange Crypto assets from messari API and use HttpResponseMessageExtension to deserialize the response
    public async Task <IEnumerable<ExchangeDto>> GetAllExchanges()
    {
        var newExchange = new List<ExchangeDto>();
        var client = new HttpClient();
        var response = client.GetAsync("https://data.messari.io/api/v1/markets").Result;
        var Exchange = await HttpResponseMessageExtensions
            .DeserializeJsonToList<ExchangeResponseDto>(response, true);
        
        _mapper.Map(Exchange, newExchange);

        return newExchange.Where(x => 
            x.AssetSymbol.Equals("BTC") || 
            x.AssetSymbol.Equals("ETH") || 
            x.AssetSymbol.Equals("XRP") || 
            x.AssetSymbol.Equals("USDT")
        );
    }
}