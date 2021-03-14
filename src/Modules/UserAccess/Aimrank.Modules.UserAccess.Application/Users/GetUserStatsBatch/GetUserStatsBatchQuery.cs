using Aimrank.Modules.UserAccess.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Modules.UserAccess.Application.Users.GetUserStatsBatch
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