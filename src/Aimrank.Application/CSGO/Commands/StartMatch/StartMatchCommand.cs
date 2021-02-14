using System;

namespace Aimrank.Application.CSGO.Commands.StartMatch
{
    public class StartMatchCommand : IServerEventCommand
    {
        public Guid MatchId { get; set; }
    }
}