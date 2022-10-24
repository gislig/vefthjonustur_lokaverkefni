using System.Collections;

namespace Cryptocop.Software.API.Services.Implementations
{
    public class ExchangeService : IExchangeService
    {
        private readonly IMessariResolverService _messariResolverService;

        public ExchangeService(IMessariResolverService messariResolverService)
        {
            _messariResolverService = messariResolverService;
            
        }
        
        public async Task<Envelope<ExchangeDto>> GetExchanges(int pageNumber = 1)
        {
            var data = await _messariResolverService.GetAllExchanges();
            
            // divide data into pages of 10 items, use pageNumber to determine which page to return
            var pages = data.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 10)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
            
            // Envelope the data
            var envelope = new Envelope<ExchangeDto>
            {
                PageNumber = pageNumber,
                Items = pages[pageNumber],
            };
            
            return envelope;
        }
    }
}