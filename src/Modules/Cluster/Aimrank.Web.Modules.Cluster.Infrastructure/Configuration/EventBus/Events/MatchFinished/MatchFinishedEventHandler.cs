using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Cluster.Application.Commands.DeleteServer;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.EventBus.Events.MatchFinished
{
    internal class MatchFinishedEventHandler : IIntegrationEventHandler<MatchFinishedEvent>
    {
        public Task HandleAsync(MatchFinishedEvent @event, CancellationToken cancellationToken = default)
            => CommandsExecutor.Execute(new DeleteServerCommand(@event.MatchId));
    }
}