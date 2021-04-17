using Aimrank.Web.Common.Domain;
using System;

namespace Aimrank.Web.Modules.Matches.Domain.Players
{
    public class PlayerId : EntityId
    {
        public PlayerId(Guid value) : base(value)
        {
        }
    }
}