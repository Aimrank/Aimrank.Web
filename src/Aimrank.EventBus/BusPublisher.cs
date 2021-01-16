using System.IO.Pipes;
using System.IO;

namespace Aimrank.EventBus
{
    public class BusPublisher : IBusPublisher
    {
        private readonly NamedPipeClientStream _client;

        public BusPublisher()
        {
            _client = new NamedPipeClientStream(".", "eventbus", PipeDirection.Out);
            _client.Connect();
        }

        public void Publish(string content)
        {
            using var writer = new StreamWriter(_client);
            
            writer.Write(content);
        }

        public void Dispose() => _client.Dispose();
    }
}