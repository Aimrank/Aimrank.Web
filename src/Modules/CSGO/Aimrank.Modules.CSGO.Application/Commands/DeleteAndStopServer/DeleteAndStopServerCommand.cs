using Aimrank.Modules.CSGO.Application.Contracts;
using System;

namespace Aimrank.Modules.CSGO.Application.Commands.DeleteAndStopServer
{
    public class DeleteAndStopServerCommand : ICommand
    {
        public Guid MatchId { get; }

        public DeleteAndStopServerCommand(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}