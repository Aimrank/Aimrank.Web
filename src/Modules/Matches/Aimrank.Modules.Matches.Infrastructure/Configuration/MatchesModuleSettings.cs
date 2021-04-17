using Aimrank.Common.Infrastructure.EventBus.RabbitMQ;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Redis;

namespace Aimrank.Modules.Matches.Infrastructure.Configuration
{
    public class MatchesModuleSettings
    {
        public RedisSettings RedisSettings { get; set; }
        public RabbitMQSettings RabbitMQSettings { get; set; }
    }
}