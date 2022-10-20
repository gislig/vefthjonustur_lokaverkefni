namespace Cryptocop.Software.API.Profiles;

public class ExchangeProfile : Profile
{
    public ExchangeProfile()
    {
        CreateMap<ExchangeDto, ExchangeResponseDto>()
            .ForMember(dest => dest.id, src => src.MapFrom(src => src.Id))
            .ForMember(dest => dest.exchange_name, src => src.MapFrom(src => src.Name))
            .ForMember(dest => dest.exchange_slug, src => src.MapFrom(src => src.Slug))
            .ForMember(dest => dest.quote_asset_symbol, src => src.MapFrom(src => src.AssetSymbol))
            .ForMember(dest => dest.price_usd, src => src.MapFrom(src => src.PriceInUsd))
            .ForMember(dest => dest.last_trade_at, src => src.MapFrom(src => src.PriceInUsd));
        CreateMap<ExchangeResponseDto, ExchangeDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(src => src.id))
            .ForMember(dest => dest.Name, src => src.MapFrom(src => src.exchange_name))
            .ForMember(dest => dest.Slug, src => src.MapFrom(src => src.exchange_slug))
            .ForMember(dest => dest.AssetSymbol, src => src.MapFrom(src => src.quote_asset_symbol))
            .ForMember(dest => dest.PriceInUsd, src => src.MapFrom(src => src.price_usd))
            .ForMember(dest => dest.LastTrade, src => src.MapFrom(src => src.last_trade_at));
    }
}