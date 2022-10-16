namespace Cryptocop.Software.API.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        
        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserDto CreateUser(RegisterInputModel inputModel)
        {
            var user = _userRepository.CreateUser(inputModel);
            return user;
        }

        public UserDto AuthenticateUser(LoginInputModel loginInputModel)
        {
            var user = _userRepository.AuthenticateUser(loginInputModel);
            return user;
        }

        // TODO: Need to implement logout functionality in repostitory
        public void Logout(int tokenId)
        {
            var token = _userRepository.LogoutUser(tokenId);
            if (token == null || token == false)
            {
                throw new Exception("Token not found or could not be deleted");
            }
        }
    }
}