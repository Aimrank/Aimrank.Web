using Aimrank.Common.Domain;
using System;

namespace Aimrank.Domain.Matches
{
    public class MatchId : EntityId
    {
        public MatchId(Guid value) : base(value)
        {
        }
    }
}