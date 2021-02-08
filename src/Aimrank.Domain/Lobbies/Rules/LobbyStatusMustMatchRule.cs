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

        public string Message => "Lobby has invalid status for this operation";
        public string Code => "invalid_lobby_status";

        public bool IsBroken() => _lobby.Status != _lobbyStatus;
    }
}