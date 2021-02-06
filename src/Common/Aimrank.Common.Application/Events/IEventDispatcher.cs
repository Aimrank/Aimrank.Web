using System.Threading.Tasks;

namespace Aimrank.Common.Application.Events
{
    public interface IEventDispatcher
    {
        void Dispatch(IIntegrationEvent @event);
        Task DispatchAsync();
    }
}