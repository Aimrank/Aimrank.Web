using System;

namespace Aimrank.Application.Queries.GetMatch
{
    public class MatchDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Map { get; set; }
        public int Status { get; set; }
    }
}