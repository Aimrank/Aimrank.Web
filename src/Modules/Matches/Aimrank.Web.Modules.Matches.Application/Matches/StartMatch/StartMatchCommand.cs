using Aimrank.Web.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Matches.StartMatch
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