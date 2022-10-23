namespace Cryptocop.Software.API.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressDto>();
        CreateMap<AddressDto, Address>();
        CreateMap<AddressInputModel, Address>();
        CreateMap<Address, AddressInputModel>();
    }
}