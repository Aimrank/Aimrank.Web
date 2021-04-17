using Aimrank.Web.Modules.CSGO.Application.Contracts;
using Aimrank.Web.Modules.CSGO.Infrastructure;
using Autofac;

namespace Aimrank.Web.App.Modules.CSGO
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