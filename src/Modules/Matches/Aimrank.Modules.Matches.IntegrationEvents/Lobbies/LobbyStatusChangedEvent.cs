using System;

namespace Aimrank.Modules.Matches.IntegrationEvents.Lobbies
{
    public class LobbyStatusChangedEvent : IntegrationEventBase
    {
        public Guid LobbyId { get; }
        public int Status { get; }

        public LobbyStatusChangedEvent(Guid lobbyId, int status)
        {
            LobbyId = lobbyId;
            Status = status;
        }
    }
}