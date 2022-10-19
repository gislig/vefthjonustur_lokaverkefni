namespace Cryptocop.Software.API.Controllers
{
    [Route("api/addresses"), Authorize]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IUserSessionService _sessionService;
        public AddressController(IAddressService addressService, IUserSessionService sessionService)
        {
            _addressService = addressService;
            _sessionService = sessionService;
        }

        /// <summary>Get all addresses from logged in user</summary>
        /// <response code="200">Returns all addresses</response>
        [SwaggerResponse(200, "Returns all addresses associated with the user", Type = typeof(IEnumerable<AddressDto>))]
        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            try{
                return Ok(_addressService.GetAllAddresses(email));
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>Add address to user</summary>
        /// <response code="200">Returns OK if it has added the address</response>
        [SwaggerResponse(200, "Adds address to a user", Type = typeof(IActionResult))]
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddressInputModel addressInput)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            
            try{
                _addressService.AddAddress(email, addressInput);
                return Ok();
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
        
        /// <summary>Remove address from user</summary>
        /// <response code="200">Returns OK if address has been removed</response>
        [SwaggerResponse(200, "Removes address from user", Type = typeof(IActionResult))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAddress(int id)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            
            try{
                _addressService.DeleteAddress(email, id);
                return Ok();
            }
            catch (Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}