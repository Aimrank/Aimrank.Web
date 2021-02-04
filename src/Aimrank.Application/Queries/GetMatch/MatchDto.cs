using System;

namespace Aimrank.Application.Queries.GetMatch
{
    public class MatchDto
    {
        public Guid Id { get; init; }
        public string Address { get; init; }
        public string Map { get; init; }
        public int Status { get; init; }
    }
}