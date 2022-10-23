namespace Cryptocop.Software.API.Profiles;

public class ShoppingChartProfile : Profile
{
    public ShoppingChartProfile()
    {
        CreateMap<ShoppingCartItem, ShoppingCartItemDto>();
        CreateMap<ShoppingCartItemDto, ShoppingCartItem>();
        CreateMap<ShoppingCartItemInputModel, ShoppingCartItemDto>();
        CreateMap<ShoppingCartItemInputModel, ShoppingCartItem>();
        CreateMap<ShoppingCartItemDto, ShoppingCartItemInputModel>();
        CreateMap<ShoppingCartItem, ShoppingCartItemInputModel>();

        CreateMap<ShoppingCart, ShoppingCartItemDto>();

        CreateMap<ShoppingCartItemDto, ShoppingCart>();
    }
}