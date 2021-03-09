using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Queries.Friendships.GetFriendship
{
    public class GetFriendshipQuery : IQuery<FriendshipDto>
    {
        public Guid UserId1 { get; }
        public Guid UserId2 { get; }

        public GetFriendshipQuery(Guid userId1, Guid userId2)
        {
            UserId1 = userId1;
            UserId2 = userId2;
        }
    }
}