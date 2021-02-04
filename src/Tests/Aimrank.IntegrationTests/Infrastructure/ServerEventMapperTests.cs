using Aimrank.Application.CSGO.Commands.FinishMatch;
using Aimrank.Application.CSGO;
using Aimrank.Infrastructure.Application.CSGO;
using System.Text.Json;
using System;
using Xunit;

namespace Aimrank.IntegrationTests.Infrastructure
{
    public class ServerEventMapperTests
    {
        private readonly IServerEventMapper _serverEventMapper = new ServerEventMapper();
        
        [Fact]
        public void ServerEventMapper_Creates_Command_If_Event_Name_Is_Tracked()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var serverEventName = "match_end";
            var serverEventData = CreateServerEvent();
            
            // Act
            var command = _serverEventMapper.Map(serverId, serverEventName, serverEventData) as FinishMatchCommand;
            
            // Assert
            Assert.NotNull(command);
            Assert.Equal(serverId, command.ServerId);
            Assert.Equal(8, command.TeamTerrorists.Score);
            Assert.Equal(0, command.TeamCounterTerrorists.Score);
        }

        [Fact]
        public void ServerEventMapper_Returns_Null_If_Event_Name_Is_Not_Tracked()
        {
            // Arrange
            var serverId = Guid.NewGuid();
            var serverEventName = "test";
            
            // Act
            var command = _serverEventMapper.Map(serverId, serverEventName, null);
            
            // Assert
            Assert.Null(command);
        }

        private static dynamic CreateServerEvent()
        {
            var data = @$"
                {{
                    ""teamTerrorists"": {{
                        ""score"": 8,
                        ""clients"": [{{
                            ""steamId"": ""12345678901234567"",
                            ""name"": ""name1"",
                            ""kills"": 8,
                            ""assists"": 0,
                            ""deaths"": 0,
                            ""score"": 16
                        }}]
                    }},
                    ""teamCounterTerrorists"": {{
                        ""score"": 0,
                        ""clients"": [{{
                            ""steamId"": ""12345678901234568"",
                            ""name"": ""name2"",
                            ""kills"": 0,
                            ""assists"": 0,
                            ""deaths"": 8,
                            ""score"": 0
                        }}]
                    }}
                }}"
                .Replace("\n", string.Empty)
                .Replace("\t", string.Empty)
                .Replace("    ", string.Empty);

            var result = JsonSerializer.Deserialize<dynamic>(data,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

            return result;
        }
    }
}