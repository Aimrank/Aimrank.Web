using Aimrank.Web.Common.Infrastructure.EventBus.RabbitMQ;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration
{
    public class ClusterModuleSettings
    {
        public RabbitMQSettings RabbitMQSettings { get; set; }
    }
}