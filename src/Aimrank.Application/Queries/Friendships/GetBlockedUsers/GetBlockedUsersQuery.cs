using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserDetails;
using System.Collections.Generic;

namespace Aimrank.Application.Queries.Friendships.GetBlockedUsers
{
    public class GetBlockedUsersQuery : IQuery<IEnumerable<UserDto>>
    {
    }
}