using System.Collections.Generic;
using System;

namespace Aimrank.Application.Queries.GetLobbyForUser
{
    public class LobbyDto
    {
        public Guid Id { get; set; }
        public Guid? MatchId { get; set; }
        public int Status { get; set; }
        public LobbyConfigurationDto Configuration { get; set; }
        public List<LobbyMemberDto> Members { get; set; }
    }

    public class LobbyConfigurationDto
    {
        public string Map { get; set; }
        public string Name { get; set; }
        public int Mode { get; set; }
    }

    public class LobbyMemberDto
    {
        public Guid UserId { get; set; }
        public bool IsLeader { get; set; }
    }
}