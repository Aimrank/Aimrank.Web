using Aimrank.Common.Application.Queries;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Application.Users.GetUserBatch;

namespace Aimrank.Modules.UserAccess.Application.Friendships.GetBlockedUsers
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