namespace Cryptocop.Software.API.Services.Interfaces;

public interface IUserSessionService
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