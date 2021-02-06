using System.Collections.Generic;
using System;

namespace Aimrank.Application.Queries.GetLobbyForUser
{
    public class LobbyDto
    {
        public Guid Id { get; set; }
        public Guid? MatchId { get; set; }
        public int Status { get; set; }
        public string Map { get; set; }
        public List<LobbyMemberDto> Members { get; set; }
    }

    public class LobbyMemberDto
    {
        public Guid UserId { get; set; }
        public bool IsLeader { get; set; }
    }
}