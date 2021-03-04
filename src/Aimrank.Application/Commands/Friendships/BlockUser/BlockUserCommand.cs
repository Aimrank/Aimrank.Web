using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Friendships.BlockUser
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