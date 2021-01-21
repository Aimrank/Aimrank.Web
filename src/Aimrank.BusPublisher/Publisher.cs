using System.IO.Pipes;
using System.IO;
using System;

namespace Aimrank.BusPublisher
{
    public class Publisher : IDisposable
    {
        private readonly NamedPipeClientStream _client;

        public Publisher(string name)
        {
            _client = new NamedPipeClientStream(".", name, PipeDirection.Out);
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