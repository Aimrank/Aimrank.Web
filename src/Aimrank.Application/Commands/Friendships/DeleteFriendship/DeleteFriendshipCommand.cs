using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Friendships.DeleteFriendship
{
    public class DeleteFriendshipCommand : ICommand
    {
        public Guid UserId { get; }

        public DeleteFriendshipCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}