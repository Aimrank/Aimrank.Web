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

        public UserToken(string token, UserTokenType type, DateTime? expiresAt = null)
        {
            Token = token;
            Type = type;
            ExpiresAt = expiresAt;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Token;
            yield return Type;
            yield return ExpiresAt;
        }
    }
}