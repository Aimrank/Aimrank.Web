using Aimrank.Web.Common.Infrastructure.EventBus.RabbitMQ;
using Aimrank.Web.Modules.Cluster.Infrastructure.Application.Events.MatchCanceled;
using Aimrank.Web.Modules.Cluster.Infrastructure.Application.Events.MatchFinished;
using Autofac;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Rabbit
{
    internal class RabbitMQModule : Autofac.Module
    {
        private readonly RabbitMQSettings _rabbitSettings;

        public RabbitMQModule(RabbitMQSettings rabbitSettings)
        {
            _rabbitSettings = rabbitSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var eventBus = new RabbitMQEventBus(_rabbitSettings);

            eventBus
                .Subscribe<MatchCanceledEvent>(builder)
                .Subscribe<MatchFinishedEvent>(builder)
                .Listen();
            
            builder.RegisterInstance(eventBus).SingleInstance();
        }
    }
}