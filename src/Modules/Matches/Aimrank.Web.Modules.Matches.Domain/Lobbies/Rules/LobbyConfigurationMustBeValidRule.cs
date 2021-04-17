using Aimrank.Web.Common.Domain;
using System.Linq;

namespace Aimrank.Web.Modules.Matches.Domain.Lobbies.Rules
{
    public class LobbyConfigurationMustBeValidRule : IBusinessRule
    {
        private readonly LobbyConfiguration _lobbyConfiguration;

        public LobbyConfigurationMustBeValidRule(LobbyConfiguration lobbyConfiguration)
        {
            _lobbyConfiguration = lobbyConfiguration;
        }

        public string Message => "Invalid lobby configuration";
        public string Code => "invalid_lobby_configuration";

        public bool IsBroken()
        {
            var supportedMaps = Maps.GetMaps();

            return !(_lobbyConfiguration.Maps.Any() && _lobbyConfiguration.Maps.All(m => supportedMaps.Contains(m)));
        }
    }
}