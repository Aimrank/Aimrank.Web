using Aimrank.Application;
using Aimrank.Common.Application;
using Aimrank.Common.Domain;

namespace Aimrank.Infrastructure.Application
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