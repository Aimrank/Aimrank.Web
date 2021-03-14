using Aimrank.Common.Application.Queries;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Application.Users.GetUserBatch;

namespace Aimrank.Modules.UserAccess.Application.Friendships.GetFriendshipInvitations
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