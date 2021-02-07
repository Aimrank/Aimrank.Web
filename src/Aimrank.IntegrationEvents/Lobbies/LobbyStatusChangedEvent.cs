using System;

namespace Aimrank.IntegrationEvents.Lobbies
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