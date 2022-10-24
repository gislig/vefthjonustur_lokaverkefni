using Newtonsoft.Json;

namespace Cryptocop.Software.API.Controllers
{
    [Route("api/orders"), Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserSessionService _sessionService;
        private readonly IQueueService _queueService;
        private readonly IConfiguration _configuration;
        private readonly IPaymentService _paymentService;

        public OrderController(IOrderService orderService, IUserSessionService sessionService, IQueueService queueService, IConfiguration configuration, IPaymentService paymentService)
        {
            _orderService = orderService;
            _sessionService = sessionService;
            _queueService = queueService;
            _configuration = configuration;
            _paymentService = paymentService;
        }

        /// <summary>Get all orders from logged in user</summary>
        /// <response code="200">Returns all orders</response>
        [SwaggerResponse(200, "Returns all orders associated with the user", Type = typeof(IEnumerable<OrderDto>))]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            try{
                var orders = _orderService.GetOrders(email);
                
                return Ok(orders);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>Add order to user</summary>
        /// <response code="200">Returns OK if it has added the order</response>
        [SwaggerResponse(200, "Adds order to a user", Type = typeof(IActionResult))]
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderInputModel orderInput)
        {
            if(!ModelState.IsValid)
                return BadRequest("Order is not valid");
            
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            try
            {
                // Send in creditcard and payment info to the queue service
                _orderService.CreateNewOrder(email, orderInput);

                //var paymentCard = _paymentService.GetStoredPaymentCards(email).Where(o => o.Id == orderInput.PaymentCardId).FirstOrDefault();
                var orders = _orderService.GetOrder(email);
                
                _queueService.PublishMessage("create-order", orders);

                return Ok();
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}