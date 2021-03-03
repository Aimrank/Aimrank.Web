using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserDetails;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Queries.Friendships.GetBlockedUsers
{
    internal class GetBlockedUsersQueryHandler : IQueryHandler<GetBlockedUsersQuery, IEnumerable<UserDto>>
    {
        public Task<IEnumerable<UserDto>> Handle(GetBlockedUsersQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}