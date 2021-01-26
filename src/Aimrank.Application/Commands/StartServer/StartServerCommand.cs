using Aimrank.Application.Contracts;
using System.Collections.Generic;
using System;

namespace Aimrank.Application.Commands.StartServer
{
    public class StartServerCommand : ICommand
    {
        public Guid ServerId { get; }
        public string Token { get; }
        public IEnumerable<string> Whitelist { get; }

        public StartServerCommand(Guid serverId, string token, IEnumerable<string> whitelist)
        {
            ServerId = serverId;
            Token = token;
            Whitelist = whitelist;
        }
    }
}