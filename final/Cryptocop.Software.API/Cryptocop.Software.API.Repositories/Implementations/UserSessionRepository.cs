namespace Cryptocop.Software.API.Repositories.Implementations;

public class UserSessionRepository : IUserSessionRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISession _session;
    //private ISession _session => _httpContextAccessor.HttpContext.Session;

    public bool CreateUserSession(UserDto user)
    {
        
        var json = JsonConvert.SerializeObject(user);
        var data = Encoding.UTF8.GetBytes(json);
        
        Console.WriteLine(user.Identifier);
        
        // save the result to session storage

        // Somehow this does not work
        // Create a key for the session
        // Save the session
        _session.Set("user", data);
        

        if (!IsUserLoggedIn())
            return false;
        
        return true;
    }
    
    public bool RemoveUserSession()
    {
        _session.Remove("user");
        if (!IsUserLoggedIn())
            return false;
        
        return true;
    }
    
    public UserDto GetUserSessionData()
    {
        if (!IsUserLoggedIn())
            return new UserDto();
            
        _session.TryGetValue("user", out byte[] data);
        string json = Encoding.UTF8.GetString(data);
                
        // Convert the json to userDto
        var userDto = JsonConvert.DeserializeObject<UserDto>(json);
                
        // Add the token to the blacklist
        //_tokenRepository.VoidToken(userDto.TokenId);
        return new UserDto();
    }

    public string GetUserToken()
    {
        return GetUserSessionData().Identifier;
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
            return _session.Keys.Contains("user");
        }
        catch
        {
            return false;
        }
    }
}