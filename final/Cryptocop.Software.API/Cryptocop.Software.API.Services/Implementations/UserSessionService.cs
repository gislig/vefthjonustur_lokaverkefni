namespace Cryptocop.Software.API.Services.Implementations;

public class UserSessionService : IUserSessionService
{
    private readonly IUserSessionRepository _userSessionRepository;

    public UserSessionService(IUserSessionRepository userSessionRepository)
    {
        _userSessionRepository = userSessionRepository;
    }

    public UserDto GetSetUserSession(string header)
    {
        return _userSessionRepository.GetSetUserSession(header);
    }

    public bool CreateUserSession(UserDto user)
    {
        return _userSessionRepository.CreateUserSession(user);
    }
    
    public bool RemoveUserSession()
    {
        return _userSessionRepository.RemoveUserSession();
    }
    
    public UserDto GetUserSessionData()
    {
        return _userSessionRepository.GetUserSessionData();
    }

    public string GetUserToken()
    {
        return _userSessionRepository.GetUserToken();
    }

    public string GetUserFullName()
    {
        return _userSessionRepository.GetUserFullName();
    }

    public string GetUserEmail()
    {
        return _userSessionRepository.GetUserEmail();
    }

    public int GetUserTokenId()
    {
        return _userSessionRepository.GetUserTokenId();
    }
}