using System;

namespace Aimrank.Web.GraphQL.Subscriptions.Messages.Users
{
    public class FriendshipInvitationCreatedMessage
    {
        public Guid InvitingUserId { get; }
        public Guid InvitedUserId { get; }

        public FriendshipInvitationCreatedMessage(Guid invitingUserId, Guid invitedUserId)
        {
            InvitingUserId = invitingUserId;
            InvitedUserId = invitedUserId;
        }
    }
}