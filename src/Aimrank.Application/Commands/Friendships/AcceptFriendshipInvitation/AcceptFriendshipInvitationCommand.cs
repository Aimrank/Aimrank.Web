using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Friendships.AcceptFriendshipInvitation
{
    public class AcceptFriendshipInvitationCommand : ICommand
    {
        public Guid InvitingUserId { get; }

        public AcceptFriendshipInvitationCommand(Guid invitingUserId)
        {
            InvitingUserId = invitingUserId;
        }
    }
}