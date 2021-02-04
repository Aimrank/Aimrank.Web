using Aimrank.Application.Contracts;
using System;

namespace Aimrank.Application.Queries.GetMatch
{
    public class GetMatchQuery : IQuery<MatchDto>
    {
        public Guid Id { get; }

        public GetMatchQuery(Guid id)
        {
            Id = id;
        }
    }
}