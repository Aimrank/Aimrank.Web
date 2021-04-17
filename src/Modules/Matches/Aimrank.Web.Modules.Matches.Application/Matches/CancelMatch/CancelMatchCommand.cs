using Aimrank.Web.Modules.Matches.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.Matches.Application.Matches.CancelMatch
{
    public class CancelMatchCommand : ICommand
    {
        public Guid MatchId { get; }

        public CancelMatchCommand(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}