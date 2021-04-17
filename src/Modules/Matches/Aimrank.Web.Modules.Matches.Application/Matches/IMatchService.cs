using Aimrank.Web.Modules.Matches.Domain.Matches;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Matches
{
    public interface IMatchService
    {
        Task AcceptMatchAsync(Match match, Guid playerId);
        Task<IEnumerable<Guid>> GetNotAcceptedPlayersAsync(MatchId matchId);
    }
}