using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.DeclineFriendshipInvitation
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