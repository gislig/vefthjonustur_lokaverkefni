namespace Cryptocop.Software.API.Repositories.Interfaces;

public interface IUserSessionRepository
{
    UserDto GetSetUserSession(string header);
    bool CreateUserSession(UserDto user);
    bool RemoveUserSession();
    UserDto GetUserSessionData();
    string GetUserToken();
    string GetUserFullName();
    string GetUserEmail();
    int GetUserTokenId();
}