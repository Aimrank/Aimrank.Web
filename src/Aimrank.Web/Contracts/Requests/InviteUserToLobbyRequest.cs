using System;

namespace Aimrank.Web.Contracts.Requests
{
    public class InviteUserToLobbyRequest
    {
        public Guid InvitedUserId { get; set; }
    }
}