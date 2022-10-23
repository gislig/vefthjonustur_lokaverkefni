using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Cryptocop.Software.API.Services.Implementations;
public class TokenService : ITokenService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expMinutes;
    private readonly ITokenRepository _tokenRepository;
    
    public TokenService(IConfiguration configuration, ITokenRepository tokenRepository)
    {
        var data = configuration["Jwt:ExpDate"];
        
        _secretKey = configuration["Jwt:SecretKey"];
        _issuer = configuration["Jwt:Issuer"];
        _audience = configuration["Jwt:Audience"];
        _expMinutes = Convert.ToInt32(data);
        _tokenRepository = tokenRepository;
    }

    public string GenerateJwtToken(UserDto user)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_secretKey));
        
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("tokenId", user.TokenId.ToString()),
            new Claim("fullName", user.FullName)
        };
        
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        
        var token = new JwtSecurityToken
        (
            claims: claims,
            expires: DateTime.Now.AddHours(_expMinutes),
            signingCredentials: creds
        );
        
        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenString;
    }

    public bool IsBlacklisted(int tokenId)
    {
        return _tokenRepository.IsTokenBlacklisted(tokenId);
    }
}

