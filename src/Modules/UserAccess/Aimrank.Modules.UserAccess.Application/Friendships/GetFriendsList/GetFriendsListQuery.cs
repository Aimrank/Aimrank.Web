using Aimrank.Common.Application.Queries;
using Aimrank.Modules.UserAccess.Application.Contracts;
using System;

namespace Aimrank.Modules.UserAccess.Application.Friendships.GetFriendsList
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