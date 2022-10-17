using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;

namespace Cryptocop.Software.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserSessionRepository _userSessionRepository;
        private readonly IUserSessionService _userSessionService;
        public AccountController(
            IAccountService accountService, 
            ITokenService tokenService, 
            ITokenRepository tokenRepository, 
            IUserSessionRepository userSessionRepository, 
            IUserSessionService userSessionService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _tokenRepository = tokenRepository;
            _userSessionRepository = userSessionRepository;
            _userSessionService = userSessionService;
        }

        /// <summary>Creates an account for user</summary>
        /// <response code="200">Returns OK if account was created</response>
        /// <response code="400">Returns Bad Request if you entered incorrect information or the user already exists</response>
        [SwaggerResponse(200, "Returns OK if account was created", Type = typeof(RegisterInputModel))]
        [HttpPost("register"), AllowAnonymous]
        public IActionResult Register([FromBody] RegisterInputModel userRegistrationInput)
        {
            var result = _accountService.CreateUser(userRegistrationInput);
            if(result.Email == null)
                return BadRequest("User already exists or something went wrong");
                
            return Ok(result);
        }
        
        /// <summary>Sign in a user</summary>
        /// <response code="200">Returns OK if Signed in</response>
        /// <response code="400">Returns Bad Request if you entered incorrect username or password</response>
        [SwaggerResponse(200, "Returns OK if account was signed in", Type = typeof(IActionResult))]
        [HttpPost("signin"), AllowAnonymous]
        public IActionResult Signin([FromBody] LoginInputModel loginInput)
        {
            UserDto user = new UserDto();
            HttpContext.Session.TryGetValue("user", out var data);
            // Convert the data back to a string
            
            if(data != null)
            {
                user = JsonConvert.DeserializeObject<UserDto>(Encoding.UTF8.GetString(data));
                if (user.Email == loginInput.Email)
                {
                    if(user.TokenId != null && _tokenService.IsBlacklisted(user.TokenId))
                        return BadRequest("User is blacklisted");
                    
                    return Ok(user.Identifier);
                }
            }

            
            // Does not work, don´t know why
            //var data = _userSessionRepository.GetUserSessionData();
            //var token = _userSessionRepository.GetUserToken();
            
            var result = _accountService.AuthenticateUser(loginInput);

            if(result.Email == null)
                return BadRequest("Incorrect username or password");
            
            if(_tokenService.IsBlacklisted(result.TokenId))
            {
                HttpContext.Session.Remove("user");
                return BadRequest("User is blacklisted");
            }
            
            // If there is no session then continue with creating a new session
            var tokenString = _tokenService.GenerateJwtToken(result);
            var tokenHandler = new JwtSecurityTokenHandler();
            //tokenHandler.WriteToken(tokenString);
            
            result.Identifier = tokenString;
            HttpContext.Session.SetString("Token", tokenString);
            // TODO: Fix this, it seems like I get a null reference exception here, I am baffled
            //var success = _userSessionRepository.CreateUserSession(result);
            //if(!success)
            //    return BadRequest("Unable to create session storage");
            var json = JsonConvert.SerializeObject(result);
            var newData = Encoding.UTF8.GetBytes(json);
            HttpContext.Session.Set("user", newData);

            return Ok(tokenString);
        }

        /// <summary>Sign out a user</summary>
        /// <response code="200">Returns OK if Signed out</response>
        /// <response code="400">Returns OK if Signed out</response>
        [SwaggerResponse(200, "Returns OK if account was signed out", Type = typeof(IActionResult))]
        // TODO: Signout
        [HttpGet("signout")]
        public IActionResult Signout()
        {

            // TODO: This does not work, I get a null reference exception, I am baffled
            /*var loggedOut = _userSessionService.RemoveUserSession();
            if(!loggedOut)
                return BadRequest("Unable to sign out");
            
            return Ok("Signed out");*/
            
            // Get the session data
            HttpContext.Session.TryGetValue("user", out byte[] data);
            
            // If data/session exists, then continue with removing the session
            if(data != null)
            {
                // Convert the data to json
                string json = Encoding.UTF8.GetString(data);
                
                // Convert the json to userDto
                var userDto = JsonConvert.DeserializeObject<UserDto>(json);
                
                // Add the token to the blacklist
                _tokenRepository.VoidToken(userDto.TokenId);

                
                // Remove the session
                HttpContext.Session.Remove("user");
            }
            else
            {
                Console.WriteLine("No session");
                return BadRequest("Unable to sign out or user already signed out.");
            }

            
            return Ok();
        }
    }
}
