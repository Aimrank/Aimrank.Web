using Aimrank.Common.Domain;

namespace Aimrank.Domain.Lobbies.Rules
{
    public class LobbyMustBeClosedRule : IBusinessRule
    {
        private readonly Lobby _lobby;

        public LobbyMustBeClosedRule(Lobby lobby)
        {
            _lobby = lobby;
        }

        public string Message => "Lobby is not closed";
        public string Code => "lobby_not_closed";

        public bool IsBroken() => _lobby.Status != LobbyStatus.Closed;
    }
}