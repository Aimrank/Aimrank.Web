using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Inbox;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Events
{
    public class InboxIntegrationEventHandler<TEvent> : IIntegrationEventHandler<TEvent>
        where TEvent : class, IIntegrationEvent
    {
        public async Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default)
        {
            using var scope = MatchesCompositionRoot.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<MatchesContext>();
            
            var type = @event.GetType().FullName;
            var data = JsonSerializer.Serialize(@event, @event.GetType());

            var message = new InboxMessage(
                @event.Id,
                @event.OccurredOn,
                type,
                data);
            
            context.InboxMessages.Add(message);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}