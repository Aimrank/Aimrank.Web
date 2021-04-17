using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.DeleteFriendship
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