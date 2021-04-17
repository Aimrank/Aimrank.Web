using Aimrank.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Modules.Matches.Application.Matches.StartMatch
{
    public class StartMatchCommand : ICommand
    {
        public Guid MatchId { get; }

        public StartMatchCommand(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}