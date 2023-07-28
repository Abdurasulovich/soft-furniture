using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Soft_furniture.Domain.Entities.Delivers;
using Soft_furniture.Service.Common.Helpers;
using Soft_furniture.Service.Interfaces.Delivers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Soft_furniture.Service.Services.AuthDelivers;

public class DeliverTokenService : IDeliverTokenService
{
    private readonly IConfiguration _config;
    public DeliverTokenService(IConfiguration configuration)
    {
        _config = configuration.GetSection("Jwt");
    }
    public string GenerateToken(Deliver deliver)
    {
        var identityClaims = new Claim[]
        {
            new Claim("Id", deliver.Id.ToString()),
            new Claim("FirstName", deliver.FirstName),
            new Claim("Lastname", deliver.LastName),
            new Claim(ClaimTypes.MobilePhone, deliver.PhoneNumber)
            //w Claim(ClaimTypes.Role, user.IdentityRole.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]!));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        int expiresHours = int.Parse(_config["Lifetime"]!);
        var token = new JwtSecurityToken(
            issuer: _config["Issuer"],
            audience: _config["Audience"],
            claims: identityClaims,
            expires: TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: keyCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
