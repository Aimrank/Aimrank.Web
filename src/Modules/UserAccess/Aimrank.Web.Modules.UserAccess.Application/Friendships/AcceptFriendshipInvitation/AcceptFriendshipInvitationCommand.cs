using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.AcceptFriendshipInvitation
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