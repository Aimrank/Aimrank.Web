using Aimrank.Application.CSGO.Commands.CancelMatch;
using Aimrank.Application.CSGO.Commands.FinishMatch;
using Aimrank.Application.CSGO.Commands.PlayerDisconnect;
using Aimrank.Application.CSGO.Commands.StartMatch;
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
            ["map_start"] = typeof(StartMatchCommand),
            ["match_end"] = typeof(FinishMatchCommand),
            ["match_cancel"] = typeof(CancelMatchCommand),
            ["player_disconnect"] = typeof(PlayerDisconnectCommand)
        };
        
        public IServerEventCommand Map(Guid matchId, string name, dynamic data)
        {
            var settings = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

            var commandType = _commands.GetValueOrDefault(name);
            if (commandType is null)
            {
                return null;
            }
            
            var content = data is null ? "{}" : JsonSerializer.Serialize<dynamic>(data, settings);
            var command = JsonSerializer.Deserialize(content, commandType, settings);
            command.MatchId = matchId;

            return command;
        }
    }
}