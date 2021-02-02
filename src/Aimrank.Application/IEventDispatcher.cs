using Aimrank.Common.Application;
using System.Threading.Tasks;

namespace Aimrank.Application
{
    public interface IEventDispatcher
    {
        void Dispatch(IntegrationEvent @event);
        Task DispatchAsync();
    }
}