using Aimrank.Web.Common.Application.Queries;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Application.Users.GetUserBatch;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.GetBlockedUsers
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