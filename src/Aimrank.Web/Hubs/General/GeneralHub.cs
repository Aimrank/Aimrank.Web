using Aimrank.Web.Attributes;
using Aimrank.Web.Hubs.Lobbies;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Aimrank.Web.Hubs.General
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