using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.UnblockUser
{
    public class UnblockUserCommand : ICommand
    {
        public Guid BlockedUserId { get; }

        public UnblockUserCommand(Guid blockedUserId)
        {
            BlockedUserId = blockedUserId;
        }
    }
}