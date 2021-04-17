using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Common.Infrastructure.EventBus.RabbitMQ;
using Autofac;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Rabbit
{
    internal static class RabbitMQEventBusAutofacExtensions
    {
        public static IRabbitMQEventBus Subscribe<T>(this IRabbitMQEventBus bus, ContainerBuilder builder)
            where T : IIntegrationEvent
        {
            bus.Subscribe<T>();

            builder.RegisterAssemblyTypes(Assemblies.Infrastructure)
                .Where(type => typeof(IIntegrationEventHandler<>).MakeGenericType(typeof(T)).IsAssignableFrom(type))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            return bus;
        }
    }
}