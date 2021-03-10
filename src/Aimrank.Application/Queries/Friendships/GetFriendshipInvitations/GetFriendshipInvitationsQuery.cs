using Aimrank.Application.Contracts;
using Aimrank.Application.Queries.Users.GetUserBatch;
using Aimrank.Common.Application.Queries;

namespace Aimrank.Application.Queries.Friendships.GetFriendshipInvitations
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