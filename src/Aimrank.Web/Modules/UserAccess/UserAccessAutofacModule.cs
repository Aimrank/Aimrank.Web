using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Infrastructure;
using Autofac;

namespace Aimrank.Web.Modules.UserAccess
{
    internal class UserAccessAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserAccessModule>()
                .As<IUserAccessModule>()
                .InstancePerLifetimeScope();
        }
    }
}