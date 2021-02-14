using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.Matches.AcceptMatch
{
    public class AcceptMatchCommand : ICommand
    {
        public Guid MatchId { get; }

        public AcceptMatchCommand(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}