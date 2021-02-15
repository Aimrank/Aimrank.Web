using Aimrank.Web.Attributes;
using Microsoft.AspNetCore.SignalR;

namespace Aimrank.Web.Hubs.General
{
    [JwtAuth]
    public class GeneralHub : Hub<IGeneralClient>
    {
    }
}