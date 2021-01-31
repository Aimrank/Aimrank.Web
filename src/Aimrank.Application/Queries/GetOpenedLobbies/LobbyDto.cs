using System.Collections.Generic;
using System;

namespace Aimrank.Application.Queries.GetOpenedLobbies
{
    public class LobbyDto
    {
        public Guid Id { get; init; }
        public int Status { get; init; }
        public string Map { get; init; }
        public List<LobbyMemberDto> Members { get; init; }
    }

    public class LobbyMemberDto
    {
        public Guid UserId { get; init; }
        public bool IsLeader { get; init; }
    }
}