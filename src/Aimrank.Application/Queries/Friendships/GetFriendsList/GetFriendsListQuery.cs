using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserDetails;
using System.Collections.Generic;
using System;

namespace Aimrank.Application.Queries.Friendships.GetFriendsList
{
    public class GetFriendsListQuery : IQuery<IEnumerable<UserDto>>
    {
        public Guid UserId { get; }

        public GetFriendsListQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}