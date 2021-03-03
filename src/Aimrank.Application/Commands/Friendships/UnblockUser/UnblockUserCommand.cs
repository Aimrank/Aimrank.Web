using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Friendships.UnblockUser
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