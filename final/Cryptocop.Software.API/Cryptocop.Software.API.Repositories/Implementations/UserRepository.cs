namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly CrytoDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;
        
        public UserRepository(CrytoDbContext dbContext, IMapper mapper, ITokenRepository tokenRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
        }

        public UserDto CreateUser(RegisterInputModel inputModel)
        {
            // Create an empty userDto object
            var userDto = new UserDto();
            
            // Check if user exists in the database
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == inputModel.Email);
            if (user != null)
            {
                return userDto;
            }
            
            // Hash the password using HashingHelper
            var hashedPassword = HashingHelper.HashPassword(inputModel.Password);

            // Map the inputModel to a new user object
            var newUser = _mapper.Map<User>(inputModel);
            newUser.HashedPassword = hashedPassword;
            
            // Save the new user to the database
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            
            // Map the new user to a userDto object
            userDto = _mapper.Map<UserDto>(newUser);

            return userDto;
        }

        public UserDto AuthenticateUser(LoginInputModel loginInputModel)
        {
            var userDto = new UserDto();
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == loginInputModel.Email);

            if (user.Email == String.Empty)
            {
                return userDto;
            }
            
            // Check if the password is correct
            var isPasswordCorrect = HashingHelper.VerifyPassword(loginInputModel.Password, user.HashedPassword);
            if (!isPasswordCorrect)
            {
                Console.WriteLine("Password is incorrect");
                return userDto;
            }

            // Map the user to a userDto object
            userDto = _mapper.Map<UserDto>(user);

            // Create a new token
            var token = _tokenRepository.CreateNewToken();
            
            // Add the token to the userDto object
            userDto.TokenId = token.Id;
            
            return userDto;
        }
        
        // TODO: LogutUser, Implement this method
        public bool LogoutUser(int tokenId)
        {
            throw new NotImplementedException();
        }
    }
}