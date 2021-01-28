using Aimrank.Common.Domain;
using System;

namespace Aimrank.Domain.RefreshTokens
{
    public class RefreshTokenId : EntityId
    {
        public RefreshTokenId(Guid value) : base(value)
        {
        }
    }
}