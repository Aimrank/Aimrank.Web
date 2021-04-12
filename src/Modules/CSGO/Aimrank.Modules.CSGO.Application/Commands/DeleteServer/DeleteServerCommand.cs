using Aimrank.Modules.CSGO.Application.Contracts;
using System;

namespace Aimrank.Modules.CSGO.Application.Commands.DeleteServer
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