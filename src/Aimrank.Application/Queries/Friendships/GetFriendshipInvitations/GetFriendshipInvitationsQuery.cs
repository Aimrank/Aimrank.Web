using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserDetails;
using System.Collections.Generic;
using System;

namespace Aimrank.Application.Queries.Friendships.GetFriendshipInvitations
{
    public class GetFriendshipInvitationsQuery : IQuery<IEnumerable<UserDto>>
    {
        public Guid UserId { get; }

        public GetFriendshipInvitationsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}