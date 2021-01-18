using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Server
{
    public class ServerProcess : IDisposable
    {
        public Guid Id { get; }

        public ServerProcessStatus Status { get; private set; }
        
        public event EventHandler<ServerProcessLogEvent> MessageReceived;
        public event EventHandler<ServerProcessStatusChangedEvent> StatusChanged;
        
        private readonly Process _process;
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public ServerProcess(Guid id)
        {
            const string command = "cd /home/steam/csgo && exec /home/steam/start.sh";
            
            Id = id;
            
            _process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{command}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
        }

        public void Start()
        {
            ChangeStatus(ServerProcessStatus.Starting);
            
            _process.Start();
            
            Task.Run(() =>
            {
                while (true)
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    var output = _process.StandardOutput.ReadLine();
                    
                    if (!string.IsNullOrEmpty(output))
                    {
                        MessageReceived?.Invoke(this, new ServerProcessLogEvent(Id, output));
                    }
                }
            });
            
            ChangeStatus(ServerProcessStatus.Running);
        }

        public void Stop()
        {
            ChangeStatus(ServerProcessStatus.Exiting);
            
            _cancellationTokenSource.Cancel();
            _process.Kill(true);
            
            ChangeStatus(ServerProcessStatus.Exited);
        }

        public void Dispose() => _process.Dispose();

        private void ChangeStatus(ServerProcessStatus status)
        {
            Status = status;
            StatusChanged?.Invoke(this, new ServerProcessStatusChangedEvent(Id, status));
        }
    }
}
