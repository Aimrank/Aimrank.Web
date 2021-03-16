using Aimrank.Modules.Matches.Infrastructure.Configuration.CSGO;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Redis;

namespace Aimrank.Modules.Matches.Infrastructure.Configuration
{
    public class MatchesModuleSettings
    {
        public CSGOSettings CSGOSettings { get; set; }
        public RedisSettings RedisSettings { get; set; }
    }
}