using Aimrank.Web.Common.Application.Events;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text;
using System;

namespace Aimrank.Web.App.Configuration.EventBus.RabbitMQ
{
    public class RabbitMQEventSerializer
    {
        private readonly JsonSerializerOptions _options = new() {PropertyNameCaseInsensitive = true};
        
        public byte[] Serialize(IIntegrationEvent @event)
            => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event, @event.GetType()));

        public IEnumerable<IIntegrationEvent> Deserialize(byte[] data, IEnumerable<Type> types)
        {
            var text = Encoding.UTF8.GetString(data);
            return types.Select(type => JsonSerializer.Deserialize(text, type, _options) as IIntegrationEvent);
        }
    }
}