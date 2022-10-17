namespace Cryptocop.Software.API.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUserSessionService _sessionService;
        private readonly IPaymentService _paymentService;
        
        public PaymentController(IUserSessionService sessionService, IPaymentService paymentService)
        {
            _sessionService = sessionService;
            _paymentService = paymentService;
        }
        
        /// <summary>Get all PaymentCards from logged in user</summary>
        /// <response code="200">Returns all payment cards</response>
        [SwaggerResponse(200, "Returns all payment cards associated with the user", Type = typeof(IEnumerable<PaymentCardDto>))]
        // TODO: Get all payment cards associated with the authenticated user
        [HttpGet, Authorize]
        public async Task<IActionResult> GetAllPayments()
        {
            var header = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var user = _sessionService.GetSetUserSession(header);
            var email = user.Email;
            
            try{
                var payments = _paymentService.GetStoredPaymentCards(email);
                return Ok(payments);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>Add payment card to the logged in user</summary>
        /// <response code="200">Return OK if payment card is added</response>
        [SwaggerResponse(200, "Returns OK if the payment card has been added to the user", Type = typeof(IActionResult))]
        // TODO: Adds a new payment card associated with the authenticated user
        [HttpPost, Authorize]
        public async Task<IActionResult> AddPayment([FromBody] PaymentCardInputModel paymentInput)
        {
            var header = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var user = _sessionService.GetSetUserSession(header);
            var email = user.Email;
            
            try{
                _paymentService.AddPaymentCard(email, paymentInput);
                return Ok();
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}