using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Friendships.DeclineFriendshipInvitation
{
    public class DeclineFriendshipInvitationCommand : ICommand
    {
        public Guid InvitingUserId { get; }

        public DeclineFriendshipInvitationCommand(Guid invitingUserId)
        {
            InvitingUserId = invitingUserId;
        }
    }
}