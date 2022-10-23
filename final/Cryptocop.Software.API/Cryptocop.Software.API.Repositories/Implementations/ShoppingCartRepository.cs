using System.Collections.Generic;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly CrytoDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;
        
        public ShoppingCartRepository(CrytoDbContext dbContext, IMapper mapper, ITokenRepository tokenRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
        }
        
        public IEnumerable<ShoppingCartItemDto> GetCartItems(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var cartItems = _dbContext
                .ShoppingCarts
                .Include(u => u.User)
                .Include(i => i.ShoppingCartItems)
                .Where(a => a.User.Email == email);

            var cart = _dbContext.ShoppingCartItems
                .Where(a => a.ShoppingCart.User.Email == email)
                .ToList();
            
            // Cant figure out in time how to calculate the total price of the items in the cart
            //var cartDto = _mapper.Map<IEnumerable<ShoppingCartItemDto>>(cart);

            // The dirty way :(
            var cartDto = new List<ShoppingCartItemDto>();
            foreach (var item in cart)
            {
                var tmp = new ShoppingCartItemDto
                {
                    Id = item.Id,
                    ProductIdentifier = item.ProductIdentifier.ToUpper(),
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.Quantity * item.UnitPrice
                    
                };
                cartDto.Add(tmp);
            }
            
            return cartDto;

        }

        public void AddCartItem(string email, ShoppingCartItemInputModel shoppingCartItemItem, float priceInUsd)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var newCartItem = new ShoppingCartItem()
            {
                ProductIdentifier = shoppingCartItemItem.ProductIdentifier.ToUpper(),
                Quantity = shoppingCartItemItem.Quantity,
                UnitPrice = priceInUsd
            };
            
            var cart = _dbContext.ShoppingCarts.FirstOrDefault(c => c.User.Email == email);
            if (cart == null)
            {
                cart = new ShoppingCart()
                {
                    User = user,
                };
                _dbContext.ShoppingCarts.Add(cart);
                _dbContext.SaveChanges();
            }   
            
            newCartItem.ShoppingCartId = cart.Id;
            _dbContext.ShoppingCartItems.Add(newCartItem);
            
            _dbContext.SaveChanges();
        }

        public void RemoveCartItem(string email, int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var cartItem = _dbContext.ShoppingCartItems.FirstOrDefault(i => i.Id == id);
            if (cartItem == null)
            {
                throw new Exception("Item not found");
            }
            _dbContext.ShoppingCartItems.Remove(cartItem);
            _dbContext.SaveChanges();
        }

        public void UpdateCartItemQuantity(string email, int id, float quantity)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var cartItem = _dbContext.ShoppingCartItems.FirstOrDefault(i => i.Id == id);
            if (cartItem == null)
            {
                throw new Exception("Item not found");
            }
            cartItem.Quantity = quantity;
            _dbContext.SaveChanges();
            
        }

        public void ClearCart(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            
            var cart = _dbContext.ShoppingCartItems.Where(i => i.ShoppingCart.User.Email == email);
            if (cart == null)
            {
                throw new Exception("Cart not found");
            }
            _dbContext.ShoppingCartItems.RemoveRange(cart);
            _dbContext.SaveChanges();
        }

        public void DeleteCart(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            
            var cart = _dbContext.ShoppingCarts.FirstOrDefault(i => i.User.Email == email);
            if (cart == null)
            {
                throw new Exception("Cart not found");
            }
            _dbContext.ShoppingCarts.Remove(cart);
            _dbContext.SaveChanges();
        }
    }
}