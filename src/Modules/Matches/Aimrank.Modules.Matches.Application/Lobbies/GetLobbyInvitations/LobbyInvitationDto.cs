using System;

namespace Aimrank.Modules.Matches.Application.Lobbies.GetLobbyInvitations
{
    public class LobbyInvitationDto
    {
        public Guid LobbyId { get; set; }
        public Guid InvitingPlayerId { get; set; }
        public Guid InvitedPlayerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}