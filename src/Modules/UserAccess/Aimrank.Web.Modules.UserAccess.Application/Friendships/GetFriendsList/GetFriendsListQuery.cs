using Aimrank.Web.Common.Application.Queries;
using Aimrank.Web.Modules.UserAccess.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.UserAccess.Application.Friendships.GetFriendsList
{
    public class GetFriendsListQuery : IQuery<PaginationDto<Guid>>
    {
        public Guid UserId { get; }
        public PaginationQuery Pagination { get; }

        public GetFriendsListQuery(Guid userId, PaginationQuery pagination)
        {
            UserId = userId;
            Pagination = pagination;
        }
    }
}