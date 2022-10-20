namespace Cryptocop.Software.API.Controllers
{
    [Route("api/payments"), Authorize]
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
        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            
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
        [HttpPost]
        public async Task<IActionResult> AddPayment([FromBody] PaymentCardInputModel paymentInput)
        {
            if(!ModelState.IsValid)
                return BadRequest("Payment data is not valid");
            
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

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