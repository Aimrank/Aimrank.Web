using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.CSGO.Application.Commands.DeleteServer;
using Aimrank.Web.Modules.CSGO.Infrastructure.Configuration;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.CSGO.Infrastructure.Application.Events.MatchFinished
{
    internal class MatchFinishedEventHandler : IIntegrationEventHandler<MatchFinishedEvent>
    {
        public Task HandleAsync(MatchFinishedEvent @event, CancellationToken cancellationToken = default)
            => CommandsExecutor.Execute(new DeleteServerCommand(@event.MatchId));
    }
}