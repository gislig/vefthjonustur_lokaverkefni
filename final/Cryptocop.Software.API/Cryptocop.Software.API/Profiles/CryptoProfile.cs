namespace Cryptocop.Software.API.Profiles;

public class CryptoProfile : Profile
{
    public CryptoProfile()
    {
        CreateMap<CryptoCurrencyDto, CryptoResponseDto>()
            .ForMember(dest => dest.id, src => src.MapFrom(src => src.Id))
            .ForMember(dest => dest.name, src => src.MapFrom(src => src.Name))
            .ForMember(dest => dest.slug, src => src.MapFrom(src => src.Slug))
            .ForMember(dest => dest.symbol, src => src.MapFrom(src => src.Symbol))
            .ForMember(dest => dest.price_usd, src => src.MapFrom(src => src.PriceInUsd))
            .ForMember(dest => dest.project_details, src => src.MapFrom(src => src.ProjectDetails));
        CreateMap<CryptoResponseDto, CryptoCurrencyDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(src => src.id))
            .ForMember(dest => dest.Name, src => src.MapFrom(src => src.name))
            .ForMember(dest => dest.Slug, src => src.MapFrom(src => src.slug))
            .ForMember(dest => dest.Symbol, src => src.MapFrom(src => src.symbol))
            .ForMember(dest => dest.PriceInUsd, src => src.MapFrom(src => src.price_usd))
            .ForMember(dest => dest.ProjectDetails, src => src.MapFrom(src => src.project_details));
    }
}