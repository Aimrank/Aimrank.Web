using Aimrank.Web.Modules.Matches.Application.Matches.CancelMatches;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Events.Events.ServersDeleted
{
    internal class ServersDeletedEventHandler : INotificationHandler<ServersDeletedEvent>
    {
        public Task Handle(ServersDeletedEvent notification, CancellationToken cancellationToken)
            => CommandsExecutor.Execute(new CancelMatchesCommand(notification.Ids));
    }
}