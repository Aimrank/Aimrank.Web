using Aimrank.Common.Domain;
using System;

namespace Aimrank.Modules.Matches.Domain.Players
{
    public class PlayerId : EntityId
    {
        public PlayerId(Guid value) : base(value)
        {
        }
    }
}