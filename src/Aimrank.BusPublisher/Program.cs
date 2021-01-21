using System.Text;
using System;

namespace Aimrank.BusPublisher
{
    class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: ./BusPublisher <server_id>");
                return 1;
            }
            
            using var client = new Publisher($"eventbus.{args[0]}");
            
            client.Publish(ReadDataFromStandardInput());

            return 0;
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