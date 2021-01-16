using System.IO.Pipes;
using System.IO;
using System;

namespace Aimrank.EventBus
{
    public class BusReceiver : IBusReceiver
    {
        public event EventHandler<BusEventArgs> MessageReceived;

        public void Listen()
        {
            while (true)
            {
                using var server = new NamedPipeServerStream("eventbus", PipeDirection.In);
                using var reader = new StreamReader(server);
                
                server.WaitForConnection();
                
                var content = reader.ReadToEnd();
                if (content.Length > 0)
                {
                    MessageReceived?.Invoke(this, new BusEventArgs(content.Trim()));
                }
                
                server.Disconnect();
            }
        }
    }
}