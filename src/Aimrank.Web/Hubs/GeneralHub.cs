using Aimrank.IntegrationEvents;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Aimrank.Web.Hubs
{
    public interface IGeneralClient
    {
        Task ServerCreated(ServerCreatedEvent @event);
    }
    
    public class GeneralHub : Hub<IGeneralClient>
    {
    }
}