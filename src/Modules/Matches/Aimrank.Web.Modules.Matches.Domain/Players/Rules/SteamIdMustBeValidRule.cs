using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.Matches.Domain.Players.Rules
{
    public class SteamIdMustBeValidRule : IBusinessRule
    {
        private readonly string _steamId;

        public SteamIdMustBeValidRule(string steamId)
        {
            _steamId = steamId;
        }

        public string Message => "SteamID is invalid";
        public string Code => "invalid_steam_id";
        
        public bool IsBroken() => string.IsNullOrEmpty(_steamId) || _steamId.Length != 17;
    }
}