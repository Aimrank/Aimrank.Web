using Aimrank.Common.Application.Events;
using Aimrank.Modules.CSGO.Application.Commands.DeleteServer;
using Aimrank.Modules.CSGO.Infrastructure.Configuration;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.CSGO.Infrastructure.Application.Events.MatchFinished
{
    internal class MatchFinishedEventHandler : IIntegrationEventHandler<MatchFinishedEvent>
    {
        public Task HandleAsync(MatchFinishedEvent @event, CancellationToken cancellationToken = default)
            => CommandsExecutor.Execute(new DeleteServerCommand(@event.MatchId));
    }
}