using Aimrank.Modules.Matches.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Modules.Matches.Application.Players.GetPlayerBatch
{
    public class GetPlayerBatchQuery : IQuery<IEnumerable<PlayerDto>>
    {
        public IEnumerable<Guid> PlayerIds { get; }

        public GetPlayerBatchQuery(IEnumerable<Guid> playerIds)
        {
            PlayerIds = playerIds;
        }
    }
}