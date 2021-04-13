using Aimrank.Modules.CSGO.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Modules.CSGO.Application.Commands.CreateServers
{
    public class CreateServersCommand : ICommand
    {
        public IEnumerable<Guid> MatchIds { get; }

        public CreateServersCommand(IEnumerable<Guid> matchIds)
        {
            MatchIds = matchIds;
        }
    }
}