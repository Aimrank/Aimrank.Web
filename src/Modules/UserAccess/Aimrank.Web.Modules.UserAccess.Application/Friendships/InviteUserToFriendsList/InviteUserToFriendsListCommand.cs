using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.InviteUserToFriendsList
{
    public class InviteUserToFriendsListCommand : ICommand
    {
        public Guid InvitedUserId { get; }

        public InviteUserToFriendsListCommand(Guid invitedUserId)
        {
            InvitedUserId = invitedUserId;
        }
    }
}