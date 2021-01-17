using Aimrank.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Web.Services
{
    public class ShellBackgroundService : BackgroundService
    {
        private readonly IHubContext<GameHub, IGameClient> _hubContext;
        private readonly ILogger<ShellBackgroundService> _logger;

        public ShellBackgroundService(
            IHubContext<GameHub, IGameClient> hubContext,
            ILogger<ShellBackgroundService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            const string command = "cd /home/steam/csgo && exec /home/steam/start.sh";
            
            var process = new Process
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

            return Task.Run(
                () =>
                {
                    try
                    {
                        process.Start();
                        
                        while (true)
                        {
                            var line = process.StandardOutput.ReadLine();

                            if (!string.IsNullOrEmpty(line))
                            {
                                _hubContext.Clients.All.ServerMessageReceived(line);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }
                },
                stoppingToken);
        }
    }
}