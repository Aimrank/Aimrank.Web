using System;

namespace Aimrank.Web.Contracts.Lobbies
{
    public class InviteUserToLobbyRequest
    {
        public Guid InvitedUserId { get; set; }
    }
}