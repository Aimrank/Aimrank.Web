using Aimrank.Common.Domain;
using Aimrank.Domain.RefreshTokens.Rules;
using System;

namespace Aimrank.Domain.RefreshTokens
{
    public class RefreshToken : Entity
    {
        public RefreshTokenId Id { get; }
        public string UserId { get; }
        public string Jwt { get; private set; }
        public bool IsInvalidated { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        
        private RefreshToken() {}

        private RefreshToken(RefreshTokenId id, string userId, string jwt, DateTime expiresAt)
        {
            BusinessRules.Check(new JwtMustBeValidRule(jwt));

            Id = id;
            Jwt = jwt;
            UserId = userId;
            ExpiresAt = expiresAt;
        }

        public static RefreshToken Create(string userId, string email, IJwtService jwtService)
            => new RefreshToken(
                new RefreshTokenId(Guid.NewGuid()),
                userId,
                jwtService.CreateJwt(userId, email),
                DateTime.UtcNow.AddMonths(6));

        public void Refresh(string jwt)
        {
            BusinessRules.Check(new JwtMustBeValidRule(jwt));
            BusinessRules.Check(new RefreshTokenMustBeValidRule(this));

            Jwt = jwt;
        }

        public void Invalidate()
        {
            BusinessRules.Check(new RefreshTokenMustBeValidRule(this));

            IsInvalidated = true;
        }

        public bool IsValid() => !IsInvalidated && ExpiresAt > DateTime.UtcNow;
    }
}