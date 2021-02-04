using Aimrank.IntegrationEvents;
using Aimrank.Web.Attributes;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Aimrank.Web.Hubs
{
    public interface IGeneralClient
    {
        Task ServerCreated(ServerCreatedEvent @event);
    }
    
    [JwtAuth]
    public class GeneralHub : Hub<IGeneralClient>
    {
    }
}