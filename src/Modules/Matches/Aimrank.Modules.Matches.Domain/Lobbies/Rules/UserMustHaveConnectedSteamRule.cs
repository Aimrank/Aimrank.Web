using Aimrank.Common.Domain;

namespace Aimrank.Modules.Matches.Domain.Lobbies.Rules
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