using Aimrank.Web.Common.Infrastructure.EventBus.RabbitMQ;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Redis;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration
{
    public class MatchesModuleSettings
    {
        public RedisSettings RedisSettings { get; set; }
        public RabbitMQSettings RabbitMQSettings { get; set; }
    }
}