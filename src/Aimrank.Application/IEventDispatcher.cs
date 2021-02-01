using Aimrank.Common.Application;
using System.Threading.Tasks;

namespace Aimrank.Application
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(IntegrationEvent @event);
    }
}