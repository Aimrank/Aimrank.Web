using Aimrank.IntegrationEvents;
using Aimrank.Web.Attributes;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Aimrank.Web.Hubs
{
    public interface IGeneralClient
    {
        Task MatchStarting(MatchStartingEvent @event);
        Task MatchStarted(MatchStartedEvent @event);
        Task MatchFinished(MatchFinishedEvent @event);
    }
    
    [JwtAuth]
    public class GeneralHub : Hub<IGeneralClient>
    {
    }
}