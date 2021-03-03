using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Friendships.InviteUserToFriendsList
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