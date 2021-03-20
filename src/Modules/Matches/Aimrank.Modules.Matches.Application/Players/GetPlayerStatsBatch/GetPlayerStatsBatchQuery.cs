using Aimrank.Modules.Matches.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Modules.Matches.Application.Players.GetPlayerStatsBatch
{
    public class GetPlayerStatsBatchQuery : IQuery<IEnumerable<PlayerStatsDto>>
    {
        public IEnumerable<Guid> PlayerIds { get; }

        public GetPlayerStatsBatchQuery(IEnumerable<Guid> playerIds)
        {
            PlayerIds = playerIds;
        }
    }
}