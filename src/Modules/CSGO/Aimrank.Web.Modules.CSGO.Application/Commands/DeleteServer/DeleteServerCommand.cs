using Aimrank.Web.Modules.CSGO.Application.Contracts;
using System;

namespace Aimrank.Web.Modules.CSGO.Application.Commands.DeleteServer
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