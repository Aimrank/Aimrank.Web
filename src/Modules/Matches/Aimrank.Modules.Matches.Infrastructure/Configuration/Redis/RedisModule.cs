using Autofac;
using StackExchange.Redis;

namespace Aimrank.Modules.Matches.Infrastructure.Configuration.Redis
{
    internal class RedisModule : Autofac.Module
    {
        private readonly RedisSettings _redisSettings;

        public RedisModule(RedisSettings redisSettings)
        {
            _redisSettings = redisSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var connectionMultiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = {_redisSettings.Endpoint},
                DefaultDatabase = _redisSettings.Database
            });

            builder.RegisterInstance(connectionMultiplexer).As<IConnectionMultiplexer>().SingleInstance();
        }
    }
}