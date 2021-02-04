using Aimrank.Application.CSGO.Commands.FinishMatch;
using Aimrank.Application.CSGO;
using System.Collections.Generic;
using System.Text.Json;
using System;

namespace Aimrank.Infrastructure.Application.CSGO
{
    internal class ServerEventMapper : IServerEventMapper
    {
        private readonly Dictionary<string, Type> _commands = new()
        {
            ["match_end"] = typeof(FinishMatchCommand)
        };
        
        public IServerEventCommand Map(Guid serverId, string name, dynamic data)
        {
            var settings = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

            var commandType = _commands.GetValueOrDefault(name);
            if (commandType is null)
            {
                return null;
            }
            
            var content = JsonSerializer.Serialize<dynamic>(data, settings);
            var command = JsonSerializer.Deserialize(content, commandType, settings);
            command.ServerId = serverId;

            return command;
        }
    }
}