namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class TokenRepository : ITokenRepository
    {
        private readonly CrytoDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public TokenRepository(CrytoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
       
        public JwtTokenDto CreateNewToken()
        {
            var tokenDto = new JwtTokenDto();
            
            // AutoMap JwtTokenDto to token
            var token = _mapper.Map<JwtToken>(tokenDto);

            _dbContext.JwtTokens.Add(token);
            _dbContext.SaveChanges();
            
            // AutoMap token to JwtTokenDto
            tokenDto = _mapper.Map<JwtTokenDto>(token);
            
            return tokenDto;
        }

        public bool IsTokenBlacklisted(int tokenId)
        {
            var token = _dbContext.JwtTokens.FirstOrDefault(t => t.Id == tokenId);
            if(token == null) return true;
            return token.Blacklisted;
        }

        public void VoidToken(int tokenId)
        {
            var token = _dbContext.JwtTokens.FirstOrDefault(t => t.Id == tokenId);
            if(token == null) return;
            token.Blacklisted = true;
            _dbContext.SaveChanges();
        }
    }
}