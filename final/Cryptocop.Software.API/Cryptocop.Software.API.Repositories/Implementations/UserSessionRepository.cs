using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;

namespace Cryptocop.Software.API.Repositories.Implementations;

public class UserSessionRepository : IUserSessionRepository
{
    private HttpContext _httpContext;
    
    public UserSessionRepository(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext;
    }

    public UserDto GetSetUserSession(string header)
    {
            
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(header);
            
        var email = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
        var name = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
        var fullName = jwtSecurityToken.Claims.First(claim => claim.Type == "fullName").Value;
        var tokenId = jwtSecurityToken.Claims.First(claim => claim.Type == "tokenId").Value;
        
        var user = new UserDto
        {
            Email = email,
            FullName = fullName,
            TokenId = Convert.ToInt32(tokenId),
            Identifier = header
        };
        CreateUserSession(user);
        return user;
    }
    
    public bool CreateUserSession(UserDto user)
    {
        
        var json = JsonConvert.SerializeObject(user);
        var data = Encoding.UTF8.GetBytes(json);
        
        _httpContext.Session.Set("user", data);

        if (!IsUserLoggedIn())
            return false;
        
        return true;
    }
    
    public bool RemoveUserSession()
    {
        _httpContext.Session.Remove("user");
        if (!IsUserLoggedIn())
            return false;
        
        return true;
    }
    
    public UserDto GetUserSessionData()
    {
        
        if (!IsUserLoggedIn())
            return new UserDto();
            
                
        // Convert the json to userDto
                
        // Add the token to the blacklist
        //_tokenRepository.VoidToken(userDto.TokenId);
        return new UserDto();
    }

    public string GetUserToken()
    {
        // Get user token from header
        var userData = _httpContext.Session.TryGetValue("user", out var token);
        Console.WriteLine("empty?" + token);
        
        // Get data from principal claims
        return Encoding.UTF8.GetString(token);
    }

    public string GetUserFullName()
    {
        return GetUserSessionData().FullName;
    }

    public string GetUserEmail()
    {
        return GetUserSessionData().Email;
    }

    public int GetUserTokenId()
    {
        return GetUserSessionData().TokenId;
    }
    
    private bool IsUserLoggedIn()
    {
        try
        {
            return _httpContext.Session.Keys.Contains("user");
        }
        catch
        {
            return false;
        }
    }
}