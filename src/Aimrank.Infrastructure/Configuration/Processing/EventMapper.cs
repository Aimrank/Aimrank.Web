using Aimrank.Common.Application.Events;
using Aimrank.Common.Domain;

namespace Aimrank.Infrastructure.Configuration.Processing
{
    internal class EventMapper : IEventMapper
    {
        public IntegrationEvent Map(IDomainEvent @event)
            => @event switch
            {
                _ => null
            };
    }
}