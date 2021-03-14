using Aimrank.Modules.UserAccess.Application.Contracts;
using System;

namespace Aimrank.Modules.UserAccess.Application.Friendships.UnblockUser
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