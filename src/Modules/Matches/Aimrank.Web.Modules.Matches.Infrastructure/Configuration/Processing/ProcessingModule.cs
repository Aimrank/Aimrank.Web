using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Common.Domain;
using Aimrank.Web.Common.Infrastructure;
using Autofac;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing
{
    internal class ProcessingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<EventMapper>().As<IEventMapper>().SingleInstance();
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