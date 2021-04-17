using Aimrank.Web.Modules.Cluster.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.DeleteServer
{
    public class DeleteServerCommand : ICommand
    {
        public Guid MatchId { get; }

        public DeleteServerCommand(Guid matchId)
        {
            MatchId = matchId;
        }
    }
}