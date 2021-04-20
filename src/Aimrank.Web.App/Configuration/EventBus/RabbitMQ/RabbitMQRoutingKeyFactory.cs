using System.Reflection;

namespace Aimrank.Web.App.Configuration.EventBus.RabbitMQ
{
    internal class RabbitMQRoutingKeyFactory
    {
        private readonly RabbitMQSettings _rabbitMqSettings;

        public RabbitMQRoutingKeyFactory(RabbitMQSettings rabbitMqSettings)
        {
            _rabbitMqSettings = rabbitMqSettings;
        }

        public string Create(MemberInfo type, string serviceName = null)
            => $"{serviceName ?? _rabbitMqSettings.ServiceName}.{type.Name}";
    }
}