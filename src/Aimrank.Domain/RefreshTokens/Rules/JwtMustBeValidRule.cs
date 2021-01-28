using Aimrank.Common.Domain;

namespace Aimrank.Domain.RefreshTokens.Rules
{
    public class JwtMustBeValidRule : IBusinessRule
    {
        private readonly string _jwt;

        public JwtMustBeValidRule(string jwt)
        {
            _jwt = jwt;
        }

        public string Message => "Provided JWT is invalid";
        public string Code => "invalid_jwt";

        public bool IsBroken() => string.IsNullOrWhiteSpace(_jwt);
    }
}