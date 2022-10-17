namespace Cryptocop.Software.API.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderDto, Order>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderInputModel, Order>();
        CreateMap<Order, OrderInputModel>();
        CreateMap<OrderInputModel, OrderDto>();
        CreateMap<OrderDto, OrderInputModel>();       
    }
}