using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Inbox;
using Autofac;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.EventBus
{
    internal class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T>
        where T : IIntegrationEvent
    {
        public async Task HandleAsync(T @event, CancellationToken cancellationToken = default)
        {
            await using var scope = MatchesCompositionRoot.BeginLifetimeScope();
            await using var context = scope.Resolve<MatchesContext>();

            var type = @event.GetType().FullName;
            var data = JsonSerializer.Serialize(@event, @event.GetType());

            var inboxMessage = new InboxMessage(
                @event.Id,
                @event.OccurredAt,
                type,
                data);
            
            context.InboxMessages.Add(inboxMessage);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}