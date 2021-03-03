using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserDetails;
using System.Collections.Generic;
using System;

namespace Aimrank.Application.Queries.Friendships.GetBlockedUsers
{
    public class GetBlockedUsersQuery : IQuery<IEnumerable<UserDto>>
    {
        public Guid UserId { get; }

        public GetBlockedUsersQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}