using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Common.Application.Events
{
    public interface IEventBus
    {
        IEnumerable<Type> Events { get; }
        Task Publish(IIntegrationEvent @event);
        Task Publish(IEnumerable<IIntegrationEvent> events);
        IEventBus Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IIntegrationEvent;
    }
}