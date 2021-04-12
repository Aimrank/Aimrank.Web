using System.Threading.Tasks;

namespace Aimrank.Common.Application.Events
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(IIntegrationEvent @event);
        Task DispatchAsync();
    }
}