using Microsoft.AspNetCore.SignalR;
using System.Linq;

namespace Aimrank.Web.Hubs
{
    public class HubUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
            => connection.User?.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
    }
}