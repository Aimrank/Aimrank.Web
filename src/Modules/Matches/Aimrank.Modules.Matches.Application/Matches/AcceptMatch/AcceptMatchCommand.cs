using Aimrank.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Modules.Matches.Application.Matches.AcceptMatch
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