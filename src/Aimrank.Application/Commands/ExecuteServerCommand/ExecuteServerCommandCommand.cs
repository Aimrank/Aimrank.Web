using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.ExecuteServerCommand
{
    public class ExecuteServerCommandCommand : ICommand
    {
        public Guid ServerId { get; }
        public string Command { get; }

        public ExecuteServerCommandCommand(Guid serverId, string command)
        {
            ServerId = serverId;
            Command = command;
        }
    }
}