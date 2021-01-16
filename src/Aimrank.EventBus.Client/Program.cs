﻿using System.Text;
using System;

namespace Aimrank.EventBus.Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            using var client = new BusPublisher();
            
            client.Publish(ReadDataFromStandardInput());
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