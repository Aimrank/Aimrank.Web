using System.Diagnostics;
using System.Threading.Tasks;
using System;

namespace Aimrank.Modules.Matches.Infrastructure.Application.CSGO
{
    internal class ServerProcess : IDisposable
    {
        private readonly Process _process;
        
        public Guid MatchId { get; }
        public ServerConfiguration Configuration { get; }
        public bool IsRunning { get; private set; }
        
        public ServerProcess(Guid matchId, ServerConfiguration configuration)
        {
            var whitelist = string.Join(',', configuration.Whitelist);
            
            var shellCommand = $"cd /home/steam/csgo && su steam -s /home/steam/start-csgo.sh {matchId} {configuration.SteamKey} {configuration.Port} {whitelist} {configuration.Map}";
            
            MatchId = matchId;
            Configuration = configuration;
            
            _process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{shellCommand}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
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
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "su",
                    Arguments = $"- steam -c \"screen -p 0 -S {MatchId} -X {command}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };

            process.Start();
            
            return process.WaitForExitAsync();
        }
    }
}
