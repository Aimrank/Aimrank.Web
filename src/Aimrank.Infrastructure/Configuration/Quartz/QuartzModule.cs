using Autofac;
using Quartz;

namespace Aimrank.Infrastructure.Configuration.Quartz
{
    internal class QuartzModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(x => typeof(IJob).IsAssignableFrom(x)).InstancePerDependency();
        }
    }
}