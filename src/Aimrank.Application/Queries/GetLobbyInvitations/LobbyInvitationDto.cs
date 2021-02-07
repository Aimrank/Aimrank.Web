using System;

namespace Aimrank.Application.Queries.GetLobbyInvitations
{
    public class LobbyInvitationDto
    {
        public Guid LobbyId { get; set; }
        public Guid InvitingUserId { get; set; }
        public string InvitingUserName { get; set; }
        public Guid InvitedUserId { get; set; }
        public string InvitedUserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}