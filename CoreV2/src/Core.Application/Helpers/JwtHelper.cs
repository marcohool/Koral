using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Application.Configuration;
using Core.DataAccess.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Core.Application.Helpers;

public class JwtHelper
{
    public static string GenerateToken(ApplicationUser user, JwtOptions jwtOptions)
    {
        if (user.Email is null)
        {
            throw new ArgumentNullException(user.Email);
        }

        SymmetricSecurityKey key = new(Encoding.ASCII.GetBytes(jwtOptions.SigningKey));

        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        ];

        SecurityTokenDescriptor tokenDescriptor =
            new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials,
            };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
