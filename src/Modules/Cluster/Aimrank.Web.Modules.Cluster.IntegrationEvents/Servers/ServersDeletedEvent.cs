using System.Collections.Generic;
using System;

namespace Aimrank.Web.Modules.Cluster.IntegrationEvents.Servers
{
    public class ServersDeletedEvent : IntegrationEventBase
    {
        public IEnumerable<Guid> MatchIds { get; }

        public ServersDeletedEvent(IEnumerable<Guid> matchIds)
        {
            MatchIds = matchIds;
        }
    }
}