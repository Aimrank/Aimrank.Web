using System;

namespace Aimrank.Web.Hubs.General.Messages
{
    public class FriendshipInvitationCreatedEventMessage
    {
        public Guid InvitingUserId { get; }
        public Guid InvitedUserId { get; }

        public FriendshipInvitationCreatedEventMessage(Guid invitingUserId, Guid invitedUserId)
        {
            InvitingUserId = invitingUserId;
            InvitedUserId = invitedUserId;
        }
    }
}