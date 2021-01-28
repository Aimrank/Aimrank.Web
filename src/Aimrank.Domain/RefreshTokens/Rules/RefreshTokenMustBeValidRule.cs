using Aimrank.Common.Domain;

namespace Aimrank.Domain.RefreshTokens.Rules
{
    public class RefreshTokenMustBeValidRule : IBusinessRule
    {
        private readonly RefreshToken _refreshToken;

        public RefreshTokenMustBeValidRule(RefreshToken refreshToken)
        {
            _refreshToken = refreshToken;
        }

        public string Message => "Refresh token is invalid";
        public string Code => "invalid_refresh_token";

        public bool IsBroken() => !_refreshToken.IsValid();
    }
}