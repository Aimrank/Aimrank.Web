using Aimrank.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Modules.Matches.Application.Matches.TimeoutReadyMatch
{
    public class TimeoutReadyMatchCommand : ICommand
    {
        public Guid MatchId { get; }

        public TimeoutReadyMatchCommand(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}