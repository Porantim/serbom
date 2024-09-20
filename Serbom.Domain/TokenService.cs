using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Serbom.Domain.Model;

namespace Serbom.Domain;

public class TokenService : BaseService
{
    public TokenService() : base(){}

    public TokenService(string email) : base(email){}

    public byte[] GetKey()
    {
        return Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "");
    }

    public string Renew()
    {
        return Generate(_currentUser);
    }

    public string Generate(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(GetKey()),
            SecurityAlgorithms.HmacSha256Signature
            );

        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = ci,
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = credentials,
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

}