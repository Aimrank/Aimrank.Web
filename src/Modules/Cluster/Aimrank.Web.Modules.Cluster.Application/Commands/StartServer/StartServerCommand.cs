using Aimrank.Web.Modules.Cluster.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Web.Modules.Cluster.Application.Commands.StartServer
{
    public class StartServerCommand : ICommand<string>
    {
        public Guid MatchId { get; }
        public string Map { get; }
        public IEnumerable<string> Whitelist { get; }

        public StartServerCommand(Guid matchId, string map, IEnumerable<string> whitelist)
        {
            MatchId = matchId;
            Map = map;
            Whitelist = whitelist;
        }
    }
}