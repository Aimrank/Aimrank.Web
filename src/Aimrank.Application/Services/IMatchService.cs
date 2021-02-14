using Aimrank.Domain.Matches;
using Aimrank.Domain.Users;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Application.Services
{
    public interface IMatchService
    {
        Task AcceptMatchAsync(Match match, UserId userId);
        Task<IEnumerable<Guid>> GetNotAcceptedUsersAsync(MatchId matchId);
    }
}