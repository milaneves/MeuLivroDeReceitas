﻿using Microsoft.IdentityModel.Tokens;
using MyRecipeBook.Domain.Security.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyRecipeBook.Infrastructure.Security.Tokens.Access.Generator
{
    public class JwtTokenGenerator : IAccessTokenGenerator
    {

        private readonly uint _expirationTimeMinutes;
        private readonly string _singinKey;

        public JwtTokenGenerator(uint expirationTimeMinutes, string singinKey)
        {
            _expirationTimeMinutes = expirationTimeMinutes;
            _singinKey = singinKey;
        }

        public string Generate(Guid userIdentifier)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, userIdentifier.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        private SymmetricSecurityKey SecurityKey()
        {
            var bytes = Encoding.UTF8.GetBytes(_singinKey);
            return new SymmetricSecurityKey(bytes);
        }
    }
}
