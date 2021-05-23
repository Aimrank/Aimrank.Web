using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.UserAccess.Domain.Users.Rules
{
    public class PasswordMustMatchRule : IBusinessRule
    {
        private readonly string _oldPassword;
        private readonly string _oldPasswordHash;
        private readonly IPasswordHasher _passwordHasher;

        public PasswordMustMatchRule(string oldPassword, string oldPasswordHash, IPasswordHasher passwordHasher)
        {
            _oldPassword = oldPassword;
            _oldPasswordHash = oldPasswordHash;
            _passwordHasher = passwordHasher;
        }

        public string Message => "Old password is invalid";
        public string Code => "invalid_old_password";

        public bool IsBroken() => !_passwordHasher.VerifyPassword(_oldPasswordHash, _oldPassword);
    }
}