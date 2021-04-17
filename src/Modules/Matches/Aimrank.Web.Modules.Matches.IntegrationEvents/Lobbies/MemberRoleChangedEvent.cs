using System;

namespace Aimrank.Web.Modules.Matches.IntegrationEvents.Lobbies
{
    public class MemberRoleChangedEvent : IntegrationEventBase
    {
        public Guid LobbyId { get; }
        public Guid PlayerId { get; }
        public int Role { get; }

        public MemberRoleChangedEvent(Guid lobbyId, Guid playerId, int role)
        {
            LobbyId = lobbyId;
            PlayerId = playerId;
            Role = role;
        }
    }
}