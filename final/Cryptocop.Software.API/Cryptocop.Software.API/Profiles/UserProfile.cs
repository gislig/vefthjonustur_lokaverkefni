namespace Cryptocop.Software.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<User, RegisterInputModel>();
        CreateMap<RegisterInputModel, User>();
    }
}