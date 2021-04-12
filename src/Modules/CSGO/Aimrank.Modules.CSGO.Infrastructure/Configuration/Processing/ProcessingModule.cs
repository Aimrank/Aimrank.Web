using Aimrank.Common.Application.Events;
using Aimrank.Common.Infrastructure;
using Autofac;

namespace Aimrank.Modules.CSGO.Infrastructure.Configuration.Processing
{
    internal class ProcessingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>().InstancePerLifetimeScope();
        }
    }
}