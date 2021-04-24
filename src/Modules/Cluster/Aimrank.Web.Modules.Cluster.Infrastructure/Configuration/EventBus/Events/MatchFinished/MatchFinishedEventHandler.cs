using Aimrank.Web.Modules.Cluster.Application.Commands.DeleteServer;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.EventBus.Events.MatchFinished
{
    internal class MatchFinishedEventHandler : INotificationHandler<MatchFinishedEvent>
    {
        public Task Handle(MatchFinishedEvent notification, CancellationToken cancellationToken)
            => CommandsExecutor.Execute(new DeleteServerCommand(notification.MatchId));
    }
}