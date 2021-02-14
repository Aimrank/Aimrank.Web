using System;

namespace Aimrank.Application.Queries.GetMatchForLobby
{
    public class MatchDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Map { get; set; }
        public int Mode { get; set; }
        public int Status { get; set; }
    }
}