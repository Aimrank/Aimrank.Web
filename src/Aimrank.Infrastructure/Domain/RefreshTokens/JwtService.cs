using Aimrank.Domain.RefreshTokens;
using Aimrank.Domain.Users;
using Aimrank.Infrastructure.Configuration.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System;

namespace Aimrank.Infrastructure.Domain.RefreshTokens
{
    internal class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string CreateJwt(UserId userId, string email)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, email),
                    new Claim("id", userId.Value.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public UserId GetUserId(string jwt)
        {
            var value = GetTokenClaims(jwt).FirstOrDefault(c => c.Type == "id")?.Value;
            return string.IsNullOrEmpty(value) ? null : new UserId(Guid.Parse(value));
        }

        public string GetUserEmail(string jwt)
            => GetTokenClaims(jwt).FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;

        private static IEnumerable<Claim> GetTokenClaims(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            if (tokenHandler.CanReadToken(jwt))
            {
                var parsed = tokenHandler.ReadJwtToken(jwt);
                return parsed.Claims;
            }
            
            return Enumerable.Empty<Claim>();
        }
    }
}