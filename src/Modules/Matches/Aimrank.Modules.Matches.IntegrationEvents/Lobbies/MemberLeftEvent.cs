using System;

namespace Aimrank.Modules.Matches.IntegrationEvents.Lobbies
{
    public class MemberLeftEvent : IntegrationEventBase
    {
        public Guid LobbyId { get; }
        public Guid UserId { get; }

        public MemberLeftEvent(Guid lobbyId, Guid userId)
        {
            LobbyId = lobbyId;
            UserId = userId;
        }
    }
}