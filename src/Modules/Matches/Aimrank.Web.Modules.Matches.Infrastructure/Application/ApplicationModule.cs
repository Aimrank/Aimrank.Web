using Aimrank.Web.Modules.Matches.Application.Matches;
using Aimrank.Web.Modules.Matches.Infrastructure.Application.Matches;
using Autofac;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Application
{
    internal class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MatchService>().As<IMatchService>().InstancePerLifetimeScope();
        }
    }
}