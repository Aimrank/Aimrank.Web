using System;

namespace Aimrank.Application.CSGO.Commands.CancelMatch
{
    public class CancelMatchCommand : IServerEventCommand
    {
        public Guid MatchId { get; set; }

        public CancelMatchCommand(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}