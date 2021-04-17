using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Common.Application.Events
{
    public interface IEventMapper
    {
        IIntegrationEvent Map(IDomainEvent @event);
    }
}