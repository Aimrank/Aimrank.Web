using Aimrank.Web.Modules.Cluster.Application.Commands.DeleteServer;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.EventBus.Events.MatchCanceled
{
    internal class MatchCanceledEventHandler : INotificationHandler<MatchCanceledEvent>
    {
        public Task Handle(MatchCanceledEvent notification, CancellationToken cancellationToken)
            => CommandsExecutor.Execute(new DeleteServerCommand(notification.MatchId));
    }
}