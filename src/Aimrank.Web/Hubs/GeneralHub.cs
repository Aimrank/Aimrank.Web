using Aimrank.IntegrationEvents.Lobbies;
using Aimrank.Web.Attributes;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Aimrank.Web.Hubs
{
    public interface IGeneralClient
    {
        Task InvitationCreated(InvitationCreatedEvent @event);
    }
    
    [JwtAuth]
    public class GeneralHub : Hub<IGeneralClient>
    {
    }
}