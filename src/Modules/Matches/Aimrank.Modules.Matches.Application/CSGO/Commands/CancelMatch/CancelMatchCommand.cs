using System;

namespace Aimrank.Modules.Matches.Application.CSGO.Commands.CancelMatch
{
    public class CancelMatchCommand : IServerEventCommand
    {
        public Guid MatchId { get; }

        public CancelMatchCommand(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}