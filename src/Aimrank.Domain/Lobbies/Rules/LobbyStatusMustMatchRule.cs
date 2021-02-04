using Aimrank.Common.Domain;

namespace Aimrank.Domain.Lobbies.Rules
{
    public class LobbyStatusMustMatchRule : IBusinessRule
    {
        private readonly Lobby _lobby;
        private readonly LobbyStatus _lobbyStatus;

        public LobbyStatusMustMatchRule(Lobby lobby, LobbyStatus lobbyStatus)
        {
            _lobby = lobby;
            _lobbyStatus = lobbyStatus;
        }

        public string Message => "Lobby is not closed";
        public string Code => "lobby_not_closed";

        public bool IsBroken() => _lobby.Status != _lobbyStatus;
    }
}