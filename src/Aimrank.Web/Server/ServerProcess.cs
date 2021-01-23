using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Server
{
    public record ServerConfiguration(string Token, int Port, List<string> Whitelist);
    
    public class ServerProcess : IDisposable
    {
        private readonly Process _process;
        
        public Guid Id { get; }
        public ServerConfiguration Configuration { get; }
        public bool IsRunning { get; private set; }
        
        public ServerProcess(Guid id, ServerConfiguration configuration)
        {
            var whitelist = string.Join(',', configuration.Whitelist);
            
            var shellCommand = $"cd /home/steam/csgo && exec /home/steam/start.sh {id} {configuration.Token} {configuration.Port} {whitelist}";
            
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

            IsRunning = true;
        }

        public Task ExecuteAsync(string command) => ExecuteScreenCommandAsync(@$"eval 'stuff \""{command}\""\015'");

        public async Task StopAsync()
        {
            if (IsRunning)
            {
                await ExecuteScreenCommandAsync("quit");
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
    }
}
