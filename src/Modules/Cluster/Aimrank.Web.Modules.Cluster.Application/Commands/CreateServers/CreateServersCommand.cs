using Aimrank.Web.Modules.Cluster.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.CreateServers
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