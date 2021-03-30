using Aimrank.Common.Domain;
using System.Collections.Generic;
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
        {
            const string token = "Test";
            
            return new UserToken(token, type, expiresAt);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Token;
            yield return Type;
            yield return ExpiresAt;
        }
    }
}