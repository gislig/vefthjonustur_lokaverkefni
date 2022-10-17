namespace Cryptocop.Software.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserSessionService _sessionService;

        public OrderController(IOrderService orderService, IUserSessionService sessionService)
        {
            _orderService = orderService;
            _sessionService = sessionService;
        }

        /// <summary>Get all orders from logged in user</summary>
        /// <response code="200">Returns all orders</response>
        [SwaggerResponse(200, "Returns all orders associated with the user", Type = typeof(IEnumerable<OrderDto>))]
        // TODO: Get all orders associated with the authenticated user
        [HttpGet, Authorize]
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
        // TODO: Add a new order associated with the authenticated ser, see Models section for reference. Remember to update FRomBody.
        [HttpPost, Authorize]
        public async Task<IActionResult> AddOrder([FromBody] OrderInputModel orderInput)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            
            try{
                _orderService.CreateNewOrder(email, orderInput);
                return Ok();
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}