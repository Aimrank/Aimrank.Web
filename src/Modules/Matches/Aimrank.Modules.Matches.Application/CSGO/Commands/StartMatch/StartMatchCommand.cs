using System;

namespace Aimrank.Modules.Matches.Application.CSGO.Commands.StartMatch
{
    public class StartMatchCommand : IServerEventCommand
    {
        public Guid MatchId { get; }

        public StartMatchCommand(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}