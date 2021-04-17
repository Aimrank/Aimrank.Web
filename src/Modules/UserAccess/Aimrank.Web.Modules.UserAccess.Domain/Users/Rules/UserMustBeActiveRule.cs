using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.UserAccess.Domain.Users.Rules
{
    public class UserMustBeActiveRule : IBusinessRule
    {
        private readonly User _user;

        public UserMustBeActiveRule(User user)
        {
            _user = user;
        }

        public string Message => "User account is not activated";
        public string Code => "user_account_not_active";

        public bool IsBroken() => !_user.IsActive;
    }
}