using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.ProcessServerEvent
{
    public class ProcessServerEventCommand : ICommand
    {
        public Guid ServerId { get; }
        public string Name { get; }
        public dynamic Data { get; }

        public ProcessServerEventCommand(Guid serverId, string name, dynamic data)
        {
            ServerId = serverId;
            Name = name;
            Data = data;
        }
    }
}