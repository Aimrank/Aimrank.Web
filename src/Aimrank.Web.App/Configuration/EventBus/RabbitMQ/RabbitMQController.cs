using Aimrank.Web.App.Configuration.Controllers;
using Aimrank.Web.Common.Application.Events;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace Aimrank.Web.App.Configuration.EventBus.RabbitMQ
{
    [ApiController]
    [DevelopmentController]
    [Route("api/[controller]")]
    public class RabbitMQController : ControllerBase
    {
        private readonly RabbitMQEventSerializer _eventSerializer;
        private readonly RabbitMQEventRegistry _eventRegistry;
        private readonly IEventBus _eventBus;

        public RabbitMQController(
            RabbitMQEventSerializer eventSerializer,
            RabbitMQEventRegistry eventRegistry,
            IEventBus eventBus)
        {
            _eventSerializer = eventSerializer;
            _eventRegistry = eventRegistry;
            _eventBus = eventBus;
        }

        /// <summary>
        /// Process "fake" RabbitMQ event that would normally come from message broker.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ProcessEvent(ProcessEventRequest request)
        {
            var types = _eventRegistry.GetEventsForRoutingKey(request.RoutingKey).ToList();
            if (types.Count == 0)
            {
                return BadRequest();
            }

            var data = JsonSerializer.Serialize(request.Data);

            var events = _eventSerializer.Deserialize(Encoding.UTF8.GetBytes(data), types);
            
            foreach (var @event in events)
            {
                await _eventBus.Publish(@event);
            }
            
            return Ok();
        }
    }
    
    public class ProcessEventRequest
    {
        public string RoutingKey { get; set; }
        public object Data { get; set; }
    }
}