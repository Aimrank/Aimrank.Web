using Aimrank.Web.Modules.CSGO.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.CSGO.Application.Commands.DeleteAndStopServer
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