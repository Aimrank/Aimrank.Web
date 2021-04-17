using Aimrank.Web.Common.Application.Queries;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using Aimrank.Web.Modules.UserAccess.Application.Users.GetUserBatch;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.GetFriendshipInvitations
{
    public class GetFriendshipInvitationsQuery : IQuery<PaginationDto<UserDto>>
    {
        public PaginationQuery Pagination { get; }

        public GetFriendshipInvitationsQuery(PaginationQuery pagination)
        {
            Pagination = pagination;
        }
    }
}