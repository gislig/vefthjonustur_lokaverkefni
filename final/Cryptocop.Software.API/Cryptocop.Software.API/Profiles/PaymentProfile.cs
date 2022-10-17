namespace Cryptocop.Software.API.Profiles;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<PaymentCard, PaymentCardDto>();
        CreateMap<PaymentCardDto, PaymentCard>();
        CreateMap<PaymentCardInputModel, PaymentCard>();
        CreateMap<PaymentCard, PaymentCardInputModel>();
    }
}