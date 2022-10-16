namespace Cryptocop.Software.API.Repositories.Interfaces;

public interface IUserSessionRepository
{
    bool CreateUserSession(UserDto user);
    bool RemoveUserSession();
    UserDto GetUserSessionData();
    string GetUserToken();
    string GetUserFullName();
    string GetUserEmail();
    int GetUserTokenId();
}