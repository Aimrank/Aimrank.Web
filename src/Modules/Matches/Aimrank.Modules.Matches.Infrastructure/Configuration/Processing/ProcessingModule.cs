using Aimrank.Common.Application.Events;
using Aimrank.Common.Domain;
using Aimrank.Common.Infrastructure;
using Autofac;

namespace Aimrank.Modules.Matches.Infrastructure.Configuration.Processing
{
    internal class ProcessingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<EventMapper>().As<IEventMapper>().SingleInstance();
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(
                    Assemblies.Application,
                    Assemblies.Domain,
                    Assemblies.Infrastructure)
                .Where(t => typeof(IDomainEventHandler).IsAssignableFrom(t))
                .AsClosedTypesOf(typeof(IDomainEventHandler<>));
        }
    }
}