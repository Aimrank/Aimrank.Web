using System.Threading.Tasks;

namespace Aimrank.Web.Common.Application.Events
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(IIntegrationEvent @event);
        Task DispatchAsync();
    }
}