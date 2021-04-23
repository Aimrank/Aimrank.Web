using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.EventBus.Events.MatchCanceled;
using Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.EventBus.Events.MatchFinished;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.EventBus
{
    internal static class EventBusStartup
    {
        public static void Initialize(IEventBus eventBus)
        {
            eventBus
                .Subscribe(new MatchCanceledEventHandler())
                .Subscribe(new MatchFinishedEventHandler());
        }
    }
}