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
        
        public IServerEventCommand Map(string content)
        {
            var settings = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

            var @event = JsonSerializer.Deserialize<ServerEventDto<object>>(content, settings);

            var commandType = _commands.GetValueOrDefault(@event.Name);
            if (commandType is null)
            {
                return null;
            } 
            
            dynamic serverEvent = JsonSerializer.Deserialize(
                content,
                typeof(ServerEventDto<>).MakeGenericType(commandType),
                settings);

            serverEvent.Data.ServerId = serverEvent.ServerId;

            return serverEvent.Data;
        }
    }
}