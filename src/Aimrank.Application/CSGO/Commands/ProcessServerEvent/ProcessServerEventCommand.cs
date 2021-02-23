using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.CSGO.Commands.ProcessServerEvent
{
    public class ProcessServerEventCommand : ICommand
    {
        public Guid MatchId { get; }
        public string Name { get; }
        public dynamic Data { get; }

        public ProcessServerEventCommand(Guid matchId, string name, dynamic data)
        {
            MatchId = matchId;
            Name = name;
            Data = data;
        }
    }
}