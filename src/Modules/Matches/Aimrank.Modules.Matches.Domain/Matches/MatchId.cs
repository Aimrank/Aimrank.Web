using Aimrank.Common.Domain;
using System;

namespace Aimrank.Modules.Matches.Domain.Matches
{
    public class MatchId : EntityId
    {
        public MatchId(Guid value) : base(value)
        {
        }
    }
}