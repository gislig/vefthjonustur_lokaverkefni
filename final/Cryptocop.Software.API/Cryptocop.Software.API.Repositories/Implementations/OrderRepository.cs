using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CrytoDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public OrderRepository(CrytoDbContext dbContext, IMapper mapper, ITokenRepository tokenRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }
        
        
        private IEnumerable<OrderDto> SetOrders(List<Order> orders)
        {
            // Did not have the time to figure out automapper, so I did it manually
            var orderDtos = new List<OrderDto>();
            PaymentCardHelper cardHelper = new PaymentCardHelper();
            foreach(var item in orders)
            {
                var tmpOrder = new OrderDto();
                
                tmpOrder.City = item.City;
                tmpOrder.Country = item.Country;
                tmpOrder.Email = item.Email;
                tmpOrder.Id = item.Id;
                tmpOrder.FullName = item.FullName;
                tmpOrder.HouseNumber = item.HouseNumber;
                tmpOrder.StreetName = item.StreetName;
                tmpOrder.CreditCard = cardHelper.MaskPaymentCard(item.MaskedCreditCard);
                tmpOrder.CardholderName = item.CardHolderName;
                Console.WriteLine(item.CardHolderName + " " + tmpOrder.CardholderName);
                tmpOrder.OrderDate = item.OrderDate.ToString();
                tmpOrder.TotalPrice = (float) item.TotalPrice;
                tmpOrder.ZipCode = item.ZipCode;
                
                var tmpOrderItems = new List<OrderItemDto>();
                foreach (var orderItem in item.OrderItems)
                {
                    //Console.WriteLine(orderItem.ProductIdentifier);
                    var tmpOrderItem = new OrderItemDto();
                    tmpOrderItem.Id = orderItem.Id;
                    tmpOrderItem.Quantity = (float) orderItem.Quantity;
                    tmpOrderItem.UnitPrice = (float) orderItem.UnitPrice;
                    tmpOrderItem.ProductIdentifier = orderItem.ProductIdentifier;
                    tmpOrderItem.TotalPrice = (float) orderItem.TotalPrice;
                    tmpOrderItems.Add(tmpOrderItem);
                }
                tmpOrder.OrderItems = tmpOrderItems;
                orderDtos.Add(tmpOrder);
            }

            return orderDtos;
        }

        public OrderDto GetOrder(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                throw new Exception("User not found");
            
            var order = _dbContext.Orders
                .Include(a => a.OrderItems)
                .Where(o => o.Email == email).OrderBy(a => a.Id).Last();
            
            if (order == null)
                throw new Exception("Order not found");
            
            // map order to orderDto
            var orderDto = new OrderDto();
            orderDto.Id = order.Id;
            orderDto.City = order.City;
            orderDto.Country = order.Country;
            orderDto.Email = order.Email;
            orderDto.FullName = order.FullName;
            orderDto.HouseNumber = order.HouseNumber;
            orderDto.StreetName = order.StreetName;
            orderDto.ZipCode = order.ZipCode;
            orderDto.OrderDate = order.OrderDate.ToString();
            orderDto.TotalPrice = (float) order.TotalPrice;
            orderDto.CardholderName = order.CardHolderName;
            orderDto.CreditCard = order.MaskedCreditCard;
            orderDto.CardholderName = order.CardHolderName;

            var orderItems = new List<OrderItemDto>();
            foreach(var item in order.OrderItems)
            {
                var orderItem = new OrderItemDto();
                orderItem.Id = item.Id;
                orderItem.Quantity = (float) item.Quantity;
                orderItem.UnitPrice = (float) item.UnitPrice;
                orderItem.ProductIdentifier = item.ProductIdentifier;
                orderItem.TotalPrice = (float) item.TotalPrice;
                orderItems.Add(orderItem);
            }
            
            orderDto.OrderItems = orderItems;
            
            return orderDto;
        }

        public IEnumerable<OrderDto> GetOrders(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                throw new Exception("User not found");
            
            var orders = _dbContext
                .Orders
                .Include(a => a.OrderItems)
                .Where(o => o.Email == email)
                .ToList();

            return SetOrders(orders);
        }

        public OrderDto CreateNewOrder(string email, OrderInputModel order)
        {
            Console.WriteLine("here");
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
                throw new Exception("User not found");
            
            var address = _dbContext.Addresses.FirstOrDefault(a => a.Id == order.AddressId);
            if(address == null)
                throw new Exception("Address not found");
            
            var creditCard = _dbContext.PaymentCards.FirstOrDefault(c => c.Id == order.PaymentCardId);
            if(creditCard == null)
                throw new Exception("Credit card not found");

            // Go through all shoppingCartItems and add it to the orderItems and add it to the new order
            var orderItems = new List<OrderItem>();
            var cart = _dbContext.ShoppingCartItems.Where(i => i.ShoppingCart.User.Email == email);
            if(cart == null)
                throw new Exception("No items in cart");
            
            // Get the total price of the order
            var totalPrice = 0.0;
            foreach (var item in cart)
            {
                totalPrice += item.UnitPrice * item.Quantity;
            }
            
            
            // Create new order
            var orderEntity = _mapper.Map<Order>(order);
            orderEntity.Email = email;
            orderEntity.City = address.City;
            orderEntity.Country = address.Country;
            orderEntity.HouseNumber = address.HouseNumber;
            orderEntity.StreetName = address.StreetName;
            orderEntity.ZipCode = address.ZipCode;
            orderEntity.TotalPrice = totalPrice;
            orderEntity.FullName = user.FullName;
            orderEntity.CardHolderName = creditCard.CardholderName;
            orderEntity.MaskedCreditCard = creditCard.CardNumber;
            orderEntity.OrderDate = DateTime.Now.ToUniversalTime();
            _dbContext.Orders.Add(orderEntity);
            _dbContext.SaveChanges();
            
            foreach (var item in cart)
            {
                var tmpCart = new OrderItem();
                tmpCart.OrderId = orderEntity.Id;
                tmpCart.Order = orderEntity;
                tmpCart.Quantity = item.Quantity;
                tmpCart.UnitPrice = item.UnitPrice;
                tmpCart.ProductIdentifier = item.ProductIdentifier;
                tmpCart.UnitPrice = item.UnitPrice;
                tmpCart.TotalPrice = item.Quantity * item.UnitPrice;
                
                orderItems.Add(tmpCart);
            }
            
            orderEntity.OrderItems = orderItems;
            _dbContext.OrderItems.AddRange(orderItems);
            _dbContext.SaveChanges();
            
            // Clear and delete the shopping cart using the ShoppingCartRepository
            
            _shoppingCartRepository.ClearCart(email);
            _shoppingCartRepository.DeleteCart(email);
            
            var myOrder = _dbContext
                .Orders
                .Include(i => i.OrderItems)
                .Where(o => o.Id == orderEntity.Id)
                .ToList();
                
            return SetOrders(myOrder).FirstOrDefault();
        }
    }
}