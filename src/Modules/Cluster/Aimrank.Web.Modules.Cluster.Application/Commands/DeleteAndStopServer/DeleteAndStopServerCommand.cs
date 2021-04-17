using Aimrank.Web.Modules.Cluster.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.DeleteAndStopServer
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