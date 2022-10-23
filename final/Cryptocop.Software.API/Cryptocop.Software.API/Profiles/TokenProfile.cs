namespace Cryptocop.Software.API.Profiles;

public class TokenProfile : Profile
{
    public TokenProfile()
    {
        CreateMap<JwtToken, JwtTokenDto>();
        CreateMap<JwtTokenDto, JwtToken>();
    }
}