using Aimrank.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Application.Queries.GetLobbiesForMatch
{
    public class GetLobbiesForMatchQuery : IQuery<IEnumerable<Guid>>
    {
        public Guid MatchId { get; }

        public GetLobbiesForMatchQuery(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}