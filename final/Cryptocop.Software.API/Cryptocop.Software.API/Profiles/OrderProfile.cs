namespace Cryptocop.Software.API.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderDto, Order>()
            .ForMember(a => a.OrderItems, opt => opt.MapFrom(a => a.OrderItems));
        CreateMap<Order, OrderDto>()
            .ForMember(a => a.OrderItems, opt => opt.MapFrom(a => a.OrderItems));
        
        CreateMap<OrderInputModel, Order>();
        CreateMap<Order, OrderInputModel>();
        CreateMap<OrderInputModel, OrderDto>();
        CreateMap<OrderDto, OrderInputModel>();       
    }
}