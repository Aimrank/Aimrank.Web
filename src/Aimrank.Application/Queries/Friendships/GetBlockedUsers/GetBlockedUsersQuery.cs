using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserBatch;
using Aimrank.Common.Application.Queries;

namespace Aimrank.Application.Queries.Friendships.GetBlockedUsers
{
    public class GetBlockedUsersQuery : IQuery<PaginationDto<UserDto>>
    {
        public PaginationQuery Pagination { get; }

        public GetBlockedUsersQuery(PaginationQuery pagination)
        {
            Pagination = pagination;
        }
    }
}