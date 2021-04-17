using System;

namespace Aimrank.Web.Modules.Matches.IntegrationEvents.Lobbies
{
    public class MemberLeftEvent : IntegrationEventBase
    {
        public Guid LobbyId { get; }
        public Guid PlayerId { get; }

        public MemberLeftEvent(Guid lobbyId, Guid playerId)
        {
            LobbyId = lobbyId;
            PlayerId = playerId;
        }
    }
}