using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.CSGO.Application.Commands.DeleteServer;
using Aimrank.Web.Modules.CSGO.Infrastructure.Configuration;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.CSGO.Infrastructure.Application.Events.MatchCanceled
{
    internal class MatchCanceledEventHandler : IIntegrationEventHandler<MatchCanceledEvent>
    {
        public Task HandleAsync(MatchCanceledEvent @event, CancellationToken cancellationToken = default)
            => CommandsExecutor.Execute(new DeleteServerCommand(@event.MatchId));
    }
}