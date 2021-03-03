using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserDetails;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.Friendships.GetFriendsList
{
    internal class GetFriendsListQueryHandler : IQueryHandler<GetFriendsListQuery, IEnumerable<UserDto>>
    {
        public Task<IEnumerable<UserDto>> Handle(GetFriendsListQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}