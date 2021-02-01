using Aimrank.Common.Application;
using System.Threading.Tasks;

namespace Aimrank.Application
{
    public interface IEventDispatcher
    {
        Task DispatchAsync();
        Task DispatchAsync(IntegrationEvent @event);
    }
}