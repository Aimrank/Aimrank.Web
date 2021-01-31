using Aimrank.Common.Domain;
using Aimrank.Domain.Users;

namespace Aimrank.Domain.Lobbies.Rules
{
    public class UserMustHaveConnectedSteamRule : IBusinessRule
    {
        private readonly User _user;

        public UserMustHaveConnectedSteamRule(User user)
        {
            _user = user;
        }

        public string Message => "You must have steam account connected";
        public string Code => "steam_not_connected";

        public bool IsBroken() => string.IsNullOrEmpty(_user.SteamId);
    }
}