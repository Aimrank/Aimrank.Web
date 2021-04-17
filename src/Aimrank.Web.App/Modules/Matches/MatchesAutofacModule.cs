using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Infrastructure;
using Aimrank.Web.App.Modules.Matches.Lobbies;
using Aimrank.Web.App.Modules.Matches.Matches;
using Autofac;

namespace Aimrank.Web.App.Modules.Matches
{
    internal class MatchesAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MatchesModule>()
                .As<IMatchesModule>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<MatchReadyEventHandler>().AsImplementedInterfaces();
            builder.RegisterType<MatchAcceptedEventHandler>().AsImplementedInterfaces();
            builder.RegisterType<MatchTimedOutEventHandler>().AsImplementedInterfaces();
            builder.RegisterType<MatchStartingEventHandler>().AsImplementedInterfaces();
            builder.RegisterType<MatchStartedEventHandler>().AsImplementedInterfaces();
            builder.RegisterType<MatchCanceledEventHandler>().AsImplementedInterfaces();
            builder.RegisterType<MatchFinishedEventHandler>().AsImplementedInterfaces();
            builder.RegisterType<MatchPlayerLeftEventHandler>().AsImplementedInterfaces();
            builder.RegisterType<LobbyStatusChangedEventHandler>().AsImplementedInterfaces();
            builder.RegisterType<MemberLeftEventHandler>().AsImplementedInterfaces();
            builder.RegisterType<MemberRoleChangedEventHandler>().AsImplementedInterfaces();
        }
    }
}