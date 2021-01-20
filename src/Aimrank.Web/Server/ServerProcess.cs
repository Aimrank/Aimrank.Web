using System.Diagnostics;
using System.IO.Pipes;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Server
{
    public class ServerProcess : IDisposable
    {
        private readonly Process _process;
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        
        public Guid Id { get; }
        public int Port { get; }
        
        public event EventHandler<ServerProcessMessageEvent> EventReceived;

        public ServerProcess(Guid id, int port)
        {
            var shellCommand = $"cd /home/steam/csgo && exec /home/steam/start.sh {id} {port}";
            
            Id = id;
            Port = port;
            
            _process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{shellCommand}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
        }

        public void Start()
        {
            _process.Start();

            Task.Factory.StartNew(
                () =>
                {
                    while (true)
                    {
                        _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                        
                        ProcessEvents();
                    }
                },
                _cancellationTokenSource.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }


        public Task ExecuteAsync(string command)
        {
            var shellCommand = @$"screen -p 0 -S {Id} -X eval 'stuff \""{command}\""\015'";

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{shellCommand}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };

            process.Start();
            
            return process.WaitForExitAsync();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();

            try
            {
                _process.Kill(true);
            }
            finally
            {
                _process.Dispose();
            }
        }
        
        private void ProcessEvents()
        {
            // Todo: This should listen only for events from that particular server
            
            using var stream = new NamedPipeServerStream($"eventbus", PipeDirection.In);
            using var reader = new StreamReader(stream);
            
            stream.WaitForConnection();

            var content = reader.ReadToEnd();
            if (content.Length > 0)
            {
                EventReceived?.Invoke(this, new ServerProcessMessageEvent(Id, content));
            }
            
            stream.Disconnect();
        }
    }
}
