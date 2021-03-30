using Aimrank.Common.Domain;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;

namespace Aimrank.Modules.UserAccess.Domain.Users
{
    public class UserToken : ValueObject
    {
        public string Token { get; }
        public UserTokenType Type { get; }
        public DateTime? ExpiresAt { get; }

        private UserToken(string token, UserTokenType type, DateTime? expiresAt = null)
        {
            Token = token;
            Type = type;
            ExpiresAt = expiresAt;
        }

        public static UserToken Create(UserTokenType type, DateTime? expiresAt = null)
            => new UserToken(CreateRandomToken(), type, expiresAt);

        private static string CreateRandomToken()
        {
            var bytes = new byte[64];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Token;
            yield return Type;
            yield return ExpiresAt;
        }
    }
}