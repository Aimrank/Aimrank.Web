using Aimrank.Common.Domain;

namespace Aimrank.Common.Application.Events
{
    public interface IEventMapper
    {
        IntegrationEvent Map(IDomainEvent @event);
    }
}