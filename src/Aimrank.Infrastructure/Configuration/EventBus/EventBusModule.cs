using Aimrank.Application;
using Aimrank.Common.Infrastructure.EventBus;
using Aimrank.Infrastructure.Application;
using Autofac;

namespace Aimrank.Infrastructure.Configuration.EventBus
{
    internal class EventBusModule : Autofac.Module
    {
        private readonly IEventBus _eventBus;

        public EventBusModule(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_eventBus);
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>().SingleInstance();
        }
    }
}