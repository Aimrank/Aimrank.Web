using System;

namespace Aimrank.Modules.Matches.IntegrationEvents.Lobbies
{
    public class MemberRoleChangedEvent : IntegrationEventBase
    {
        public Guid LobbyId { get; }
        public Guid UserId { get; }
        public int Role { get; }

        public MemberRoleChangedEvent(Guid lobbyId, Guid userId, int role)
        {
            LobbyId = lobbyId;
            UserId = userId;
            Role = role;
        }
    }
}