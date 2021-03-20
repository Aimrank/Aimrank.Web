using Aimrank.Modules.UserAccess.Application.Contracts;
using System;

namespace Aimrank.Modules.UserAccess.Application.Friendships.BlockUser
{
    public class BlockUserCommand : ICommand
    {
        public Guid BlockedUserId { get; }

        public BlockUserCommand(Guid blockedUserId)
        {
            BlockedUserId = blockedUserId;
        }
    }
}