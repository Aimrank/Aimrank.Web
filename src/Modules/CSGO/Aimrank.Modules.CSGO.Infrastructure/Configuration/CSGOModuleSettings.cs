using Aimrank.Common.Infrastructure.EventBus.RabbitMQ;

namespace Aimrank.Modules.CSGO.Infrastructure.Configuration
{
    public class CSGOModuleSettings
    {
        public RabbitMQSettings RabbitMQSettings { get; set; }
    }
}