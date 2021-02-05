using Aimrank.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Application.Queries.GetMatchesHistory
{
    public class GetMatchesHistoryQuery : IQuery<IEnumerable<MatchHistoryDto>>
    {
        public Guid? UserId { get; }

        public GetMatchesHistoryQuery(Guid? userId)
        {
            UserId = userId;
        }
    }
}