using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Aimrank.BusPublisher
{
    record Request(string Content);

    static class Program
    {
        public static async Task Main(string[] args)
        {
            using var httpClient = new HttpClient();
            await httpClient.PostAsJsonAsync("http://localhost/api/server", ReadDataFromStandardInput());
        }

        private static Request ReadDataFromStandardInput()
        {
            var builder = new StringBuilder();
            var content = Console.ReadLine();

            while (content is not null)
            {
                builder.AppendLine(content);
                content = Console.ReadLine();
            }

            return new Request(builder.ToString());
        }
    }
}