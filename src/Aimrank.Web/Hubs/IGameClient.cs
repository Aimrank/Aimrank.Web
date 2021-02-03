using Aimrank.IntegrationEvents;
using System.Threading.Tasks;

namespace Aimrank.Web.Hubs
{
    public interface IGameClient
    {
        Task ServerCreated(ServerCreatedEvent @event);
    }
}