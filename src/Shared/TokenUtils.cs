using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

using Backend.src.Entity;

public class TokenUtils
{
    private readonly IConfiguration _config;
    public TokenUtils(IConfiguration config)
    {
        _config = config;
    }
    public string GenerateToken(User user)
    {
        var issuer = _config.GetSection("Jwt:Issuer").Value;
        // claim collection expression
        var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

        //         (string, string)[] claims = [
        // (ClaimTypes.NameIdentifier, user.Id.ToString()),
        //     (ClaimTypes.Role, user.Role.ToString()),
        //     (ClaimTypes.Email, user.Email)
        // ];
        var audience = _config.GetSection("Jwt:Audience").Value;
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value!));
        var signingKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = audience,
            Expires = DateTime.Now.AddMinutes(60),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = signingKey
        };
        var token = tokenHandler.CreateToken(descriptor);
        return tokenHandler.WriteToken(token);
    }
}