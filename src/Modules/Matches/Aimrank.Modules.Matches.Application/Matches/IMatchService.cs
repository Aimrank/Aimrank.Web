using Aimrank.Modules.Matches.Domain.Matches;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Modules.Matches.Application.Matches
{
    public interface IMatchService
    {
        Task AcceptMatchAsync(Match match, Guid userId);
        Task<IEnumerable<Guid>> GetNotAcceptedUsersAsync(MatchId matchId);
    }
}