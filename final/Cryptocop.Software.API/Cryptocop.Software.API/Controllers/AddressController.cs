using System.Collections;
using Newtonsoft.Json;

namespace Cryptocop.Software.API.Controllers
{
    [Route("api/addresses")]
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
        [HttpGet, Authorize]
        public async Task<IEnumerable<AddressDto>> GetAddresses()
        {
            // List of empty addressesDto
            UserDto user = new UserDto();
            HttpContext.Session.TryGetValue("user", out var data);
            if (data == null){
                Console.WriteLine("No user in session");
                return null;
            }

            user = JsonConvert.DeserializeObject<UserDto>(Encoding.UTF8.GetString(data));
            Console.WriteLine(user.Email);
            
            var addresses = _addressService.GetAllAddresses(user.Email);
            return addresses;
        }
        
        /// <summary>Add address to user</summary>
        /// <response code="200">Returns OK if it has added the address</response>
        [SwaggerResponse(200, "Adds address to a user", Type = typeof(IActionResult))]
        [HttpPost, Authorize]
        public IActionResult AddAddress([FromBody] AddressInputModel addressInput)
        {
            var email = _sessionService.GetUserEmail();
            

            
            Console.WriteLine("Whats my email : " + email);
            _addressService.AddAddress(email, addressInput);
            return Ok();
        }
        
        /// <summary>Remove address from user</summary>
        /// <response code="200">Returns OK if address has been removed</response>
        [SwaggerResponse(200, "Removes address from user", Type = typeof(IActionResult))]
        [HttpDelete("{id}"), Authorize]
        public IActionResult RemoveAddress(int id)
        {
            var email = _sessionService.GetUserEmail();
            _addressService.DeleteAddress(email, id);
            return Ok();
        }
    }
}