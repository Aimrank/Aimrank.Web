using Aimrank.Modules.CSGO.Application.Contracts;
using Aimrank.Modules.CSGO.Infrastructure;
using Autofac;

namespace Aimrank.Web.Modules.CSGO
{
    internal class CSGOAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CSGOModule>()
                .As<ICSGOModule>()
                .InstancePerLifetimeScope();
        }
    }
}