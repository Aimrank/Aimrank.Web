using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Common.Infrastructure;
using Autofac;

namespace Aimrank.Web.Modules.CSGO.Infrastructure.Configuration.Processing
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