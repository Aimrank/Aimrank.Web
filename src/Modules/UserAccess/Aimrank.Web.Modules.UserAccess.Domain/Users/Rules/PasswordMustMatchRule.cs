using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.UserAccess.Domain.Users.Rules
{
    public class PasswordMustMatchRule : IBusinessRule
    {
        private readonly string _oldPassword;
        private readonly string _oldPasswordHash;

        public PasswordMustMatchRule(string oldPassword, string oldPasswordHash)
        {
            _oldPassword = oldPassword;
            _oldPasswordHash = oldPasswordHash;
        }

        public string Message => "Old password is invalid";
        public string Code => "invalid_old_password";

        public bool IsBroken() => !PasswordManager.VerifyPassword(_oldPasswordHash, _oldPassword);
    }
}