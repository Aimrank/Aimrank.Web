using Aimrank.Web.Common.Application.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;

namespace Aimrank.Web.App.Configuration.EventBus.RabbitMQ
{
    public class RabbitMQEventRegistry
    {
        private readonly Dictionary<string, List<Type>> _events = new();
        
        private readonly RabbitMQRoutingKeyFactory _routingKeyFactory;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMQEventRegistry(RabbitMQRoutingKeyFactory routingKeyFactory, IServiceProvider serviceProvider)
        {
            _routingKeyFactory = routingKeyFactory;
            _serviceProvider = serviceProvider;
            
            RegisterEventHandlers();
        }

        private void RegisterEventHandlers()
        {
            using var scope = _serviceProvider.CreateScope();

            var types = scope.ServiceProvider.GetServices(typeof(IIntegrationEventHandler))
                .Select(handler => handler?.GetType().GetGenericArguments().FirstOrDefault())
                .Where(type => type is not null)
                .Where(type =>
                {
                    var attribute =
                        (IntegrationEventAttribute) type.GetCustomAttribute(typeof(IntegrationEventAttribute));
                    return attribute is not null && attribute.Type == IntegrationEventType.Inbound;
                })
                .Select(type => new EventType(type, (IntegrationEventAttribute) type.GetCustomAttribute(typeof(IntegrationEventAttribute))))
                .ToList();

            foreach (var (type, attribute) in types)
            {
                var routingKey = _routingKeyFactory.Create(type, attribute.Service);
                if (_events.ContainsKey(routingKey))
                {
                    _events[routingKey].Add(type);
                }
                else
                {
                    _events[routingKey] = new List<Type> {type};
                }
            }
        }

        public IEnumerable<Type> GetEventsForRoutingKey(string routingKey)
            => _events.GetValueOrDefault(routingKey) ?? Enumerable.Empty<Type>();

        public IEnumerable<string> GetRoutingKeys() => _events.Keys;

        private record EventType(Type Type, IntegrationEventAttribute Attribute);
    }
}