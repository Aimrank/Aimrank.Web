using Microsoft.AspNetCore.SignalR.Client;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Aimrank.BusPublisher
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:80/hubs/game")
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            await connection.StartAsync();
            await connection.InvokeAsync("PublishEvent", ReadDataFromStandardInput());
        }

        private static string ReadDataFromStandardInput()
        {
            var builder = new StringBuilder();
            var content = Console.ReadLine();

            while (content is not null)
            {
                builder.AppendLine(content);
                content = Console.ReadLine();
            }

            return builder.ToString();
        }
    }
}