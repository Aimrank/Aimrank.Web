using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Commands.StopServer
{
    public class StopServerCommand : ICommand
    {
        public Guid ServerId { get; }

        public StopServerCommand(Guid serverId)
        {
            ServerId = serverId;
        }
    }
}