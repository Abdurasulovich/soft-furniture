﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Soft_furniture.Domain.Entities.Users;
using Soft_furniture.Service.Common.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Soft_furniture.Service.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    public TokenService(IConfiguration configuration)
    {
        _config = configuration.GetSection("Jwt");
    }
    public string GenerateToken(User user)
    {
        var identityClaims = new Claim[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.Role, user.IdentityRole.ToString())
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