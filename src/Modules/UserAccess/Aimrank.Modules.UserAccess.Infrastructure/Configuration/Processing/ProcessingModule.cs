using Aimrank.Common.Application.Events;
using Aimrank.Common.Domain;
using Aimrank.Common.Infrastructure;
using Autofac;

namespace Aimrank.Modules.UserAccess.Infrastructure.Configuration.Processing
{
    internal class ProcessingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>().InstancePerLifetimeScope();
            builder.RegisterType<DomainEventAccessor>().As<IDomainEventAccessor>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(
                    Assemblies.Application,
                    Assemblies.Domain,
                    Assemblies.Infrastructure)
                .Where(t => typeof(IDomainEventHandler).IsAssignableFrom(t))
                .AsClosedTypesOf(typeof(IDomainEventHandler<>));
        }
    }
}