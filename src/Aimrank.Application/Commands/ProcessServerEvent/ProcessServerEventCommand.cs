using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.ProcessServerEvent
{
    public class ProcessServerEventCommand : ICommand
    {
        public Guid ServerId { get; }
        public string Content { get; }

        public ProcessServerEventCommand(Guid serverId, string content)
        {
            ServerId = serverId;
            Content = content;
        }
    }
}