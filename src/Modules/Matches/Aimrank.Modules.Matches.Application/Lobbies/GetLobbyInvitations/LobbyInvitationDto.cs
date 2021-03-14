using System;

namespace Aimrank.Modules.Matches.Application.Lobbies.GetLobbyInvitations
{
    public class LobbyInvitationDto
    {
        public Guid LobbyId { get; set; }
        public Guid InvitingUserId { get; set; }
        public Guid InvitedUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}