using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Matches.TimeoutReadyMatch
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