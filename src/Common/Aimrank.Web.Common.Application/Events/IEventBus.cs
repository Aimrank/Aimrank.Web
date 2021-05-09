using System.Threading.Tasks;

namespace Aimrank.Web.Common.Application.Events
{
    public interface IEventBus
    {
        Task Publish(IIntegrationEvent @event);
    }
}