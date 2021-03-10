using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Queries;
using System;

namespace Aimrank.Application.Queries.Friendships.GetFriendsList
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