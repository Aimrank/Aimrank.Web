using Aimrank.Web.Common.Domain;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Aimrank.Web.Modules.UserAccess.Domain.Users.Rules
{
    public class UserTokenMustBeValidRule : IBusinessRule
    {
        private readonly UserTokenType _tokenType;
        private readonly IEnumerable<UserToken> _tokens;
        private readonly string _token;

        public UserTokenMustBeValidRule(
            UserTokenType tokenType,
            IEnumerable<UserToken> tokens,
            string token)
        {
            _tokenType = tokenType;
            _tokens = tokens;
            _token = token;
        }

        public string Message => "Invalid token";
        public string Code => "invalid_token";
        
        public bool IsBroken()
        {
            var token = _tokens.FirstOrDefault(t => t.Type == _tokenType);
            if (token is null)
            {
                return true;
            }

            if (token.Token != _token)
            {
                return true;
            }

            return token.ExpiresAt.HasValue && token.ExpiresAt.Value <= DateTime.UtcNow;
        }
    }
}