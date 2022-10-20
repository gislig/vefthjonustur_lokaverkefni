using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cryptocop.Software.API.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMessariResolverService _messariResolverService;
        
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IMessariResolverService messariResolverService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _messariResolverService = messariResolverService;
        }

        public IEnumerable<ShoppingCartItemDto> GetCartItems(string email)
        {
            return _shoppingCartRepository.GetCartItems(email);
        }

        public async Task AddCartItem(string email, ShoppingCartItemInputModel shoppingCartItemItem)
        {
            // TODO: Call the external API using the product identifier as an URL
            // parameter to receive the current price in USD for this particular
            // cryptocurrency
            var assets = await _messariResolverService.GetAllCryptoAssets();
            var asset = assets.First(x => x.Symbol == shoppingCartItemItem.ProductIdentifier);
            
            // TODO: Deserialize the response to a CryptoCurrencyDto model
            // TODO: Add it to the database using the appropriate repository class
            throw new System.NotImplementedException();
        }

        public void RemoveCartItem(string email, int id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCartItemQuantity(string email, int id, float quantity)
        {
            throw new System.NotImplementedException();
        }

        public void ClearCart(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}
