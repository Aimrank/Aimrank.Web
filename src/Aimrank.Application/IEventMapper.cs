using Aimrank.Common.Application;
using Aimrank.Common.Domain;

namespace Aimrank.Application
{
    public interface IEventMapper
    {
        IntegrationEvent Map(IDomainEvent @event);
    }
}