using Aimrank.Web.Common.Infrastructure.EventBus.RabbitMQ;

namespace Aimrank.Web.Modules.CSGO.Infrastructure.Configuration
{
    public class CSGOModuleSettings
    {
        public RabbitMQSettings RabbitMQSettings { get; set; }
    }
}