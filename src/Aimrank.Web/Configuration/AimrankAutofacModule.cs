using Aimrank.Application.Contracts;
using Aimrank.Infrastructure;
using Autofac;

namespace Aimrank.Web.Configuration
{
    public class AimrankAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AimrankModule>().As<IAimrankModule>().InstancePerLifetimeScope();
        }
    }
}