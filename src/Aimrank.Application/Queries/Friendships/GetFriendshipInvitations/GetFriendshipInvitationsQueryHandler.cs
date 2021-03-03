using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserDetails;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Application.Queries.Friendships.GetFriendshipInvitations
{
    internal class GetFriendshipInvitationsQueryHandler : IQueryHandler<GetFriendshipInvitationsQuery, IEnumerable<UserDto>>
    {
        public Task<IEnumerable<UserDto>> Handle(GetFriendshipInvitationsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}