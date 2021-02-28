using System;

namespace Aimrank.Application.CSGO.Commands.CancelMatch
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