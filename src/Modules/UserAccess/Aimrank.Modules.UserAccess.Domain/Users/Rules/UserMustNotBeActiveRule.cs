using Aimrank.Common.Domain;

namespace Aimrank.Modules.UserAccess.Domain.Users.Rules
{
    public class UserMustNotBeActiveRule : IBusinessRule
    {
        private readonly User _user;

        public UserMustNotBeActiveRule(User user)
        {
            _user = user;
        }

        public string Message => "User account is already activated";
        public string Code => "user_account_active";

        public bool IsBroken() => _user.IsActive;
    }
}