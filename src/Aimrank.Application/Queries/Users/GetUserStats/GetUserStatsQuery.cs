using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Queries.Users.GetUserStats
{
    public class GetUserStatsQuery : IQuery<UserStatsDto>
    {
        public Guid UserId { get; }

        public GetUserStatsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}