using Aimrank.Common.Domain;

namespace Aimrank.Domain.Lobbies.Rules
{
    public class LobbyStatusMustMatchRule : IBusinessRule
    {
        private readonly LobbyStatus _status;
        private readonly LobbyStatus _expectedStatus;

        public LobbyStatusMustMatchRule(LobbyStatus status, LobbyStatus expectedStatus)
        {
            _status = status;
            _expectedStatus = expectedStatus;
        }

        public string Message => "Lobby has invalid status for this operation";
        public string Code => "invalid_lobby_status";

        public bool IsBroken() => _status != _expectedStatus;
    }
}