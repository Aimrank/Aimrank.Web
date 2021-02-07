using Aimrank.Common.Domain;

namespace Aimrank.Common.Application.Events
{
    public interface IEventMapper
    {
        IIntegrationEvent Map(IDomainEvent @event);
    }
}