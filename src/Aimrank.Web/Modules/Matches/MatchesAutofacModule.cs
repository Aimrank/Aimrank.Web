using Aimrank.Modules.Matches.Application.Contracts;
using Aimrank.Modules.Matches.Infrastructure;
using Aimrank.Web.Modules.Matches.Lobbies;
using Aimrank.Web.Modules.Matches.Matches;
using Autofac;

namespace Aimrank.Web.Modules.Matches
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