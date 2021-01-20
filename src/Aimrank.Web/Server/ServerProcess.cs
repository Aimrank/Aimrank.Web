using System.Diagnostics;
using System.IO.Pipes;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Server
{
    public record ServerConfiguration(string Token, int Port);
    
    public class ServerProcess : IDisposable
    {
        private readonly Process _process;
        private readonly CancellationTokenSource _cancellationTokenSource = new();
        
        public Guid Id { get; }
        public ServerConfiguration Configuration { get; }
        public bool IsRunning { get; private set; }
        
        public event EventHandler<ServerProcessMessageEvent> EventReceived;

        public ServerProcess(Guid id, ServerConfiguration configuration)
        {
            var shellCommand = $"cd /home/steam/csgo && exec /home/steam/start.sh {id} {configuration.Token} {configuration.Port}";
            
            Id = id;
            Configuration = configuration;
            
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

            IsRunning = true;
        }

        public Task ExecuteAsync(string command) => ExecuteScreenCommandAsync(@$"eval 'stuff \""{command}\""\015'");

        public async Task StopAsync()
        {
            if (IsRunning)
            {
                await ExecuteScreenCommandAsync("quit");
                
                _cancellationTokenSource.Cancel();
            }

            IsRunning = false;
        }

        public void Dispose()
        {
            StopAsync().Wait();

            try
            {
                _process.Kill(true);
            }
            finally
            {
                _process.Dispose();
            }
        }
        
        private Task ExecuteScreenCommandAsync(string command)
        {
            var shellCommand = @$"screen -p 0 -S {Id} -X {command}";
            
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
        
        private void ProcessEvents()
        {
            // Todo: This should listen only for events from that particular server
            
            using var stream = new NamedPipeServerStream("eventbus", PipeDirection.In);
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
