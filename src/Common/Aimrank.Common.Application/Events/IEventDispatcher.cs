using System.Threading.Tasks;

namespace Aimrank.Common.Application.Events
{
    public interface IEventDispatcher
    {
        void Dispatch(IntegrationEvent @event);
        Task DispatchAsync();
    }
}