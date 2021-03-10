using Aimrank.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Application.Queries.Users.GetUserStatsBatch
{
    public class GetUserStatsBatchQuery : IQuery<IEnumerable<UserStatsDto>>
    {
        public IEnumerable<Guid> UserIds { get; }

        public GetUserStatsBatchQuery(IEnumerable<Guid> userIds)
        {
            UserIds = userIds;
        }
    }
}