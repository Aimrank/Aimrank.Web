using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Redis;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration
{
    public class MatchesModuleSettings
    {
        public string ClusterAddress { get; set; }
        public RedisSettings RedisSettings { get; set; }
    }
}