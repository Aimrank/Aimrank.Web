using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Processing.Inbox;
using Autofac;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.EventBus
{
    internal class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T>
        where T : IIntegrationEvent
    {
        public async Task HandleAsync(T @event, CancellationToken cancellationToken = default)
        {
            await using var scope = ClusterCompositionRoot.BeginLifetimeScope();
            await using var context = scope.Resolve<ClusterContext>();
            
            var type = @event.GetType().FullName;
            var data = JsonSerializer.Serialize(@event, @event.GetType());

            var message = new InboxMessage(
                @event.Id,
                @event.OccurredAt,
                type,
                data);
            
            context.InboxMessages.Add(message);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}