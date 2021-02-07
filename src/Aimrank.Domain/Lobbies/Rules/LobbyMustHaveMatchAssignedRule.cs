using Aimrank.Common.Domain;

namespace Aimrank.Domain.Lobbies.Rules
{
    public class LobbyMustHaveMatchAssignedRule : IBusinessRule
    {
        private readonly Lobby _lobby;

        public LobbyMustHaveMatchAssignedRule(Lobby lobby)
        {
            _lobby = lobby;
        }

        public string Message => "Lobby doesn't have assigned match";
        public string Code => "no_match_assigned";

        public bool IsBroken() => _lobby.MatchId == null;
    }
}