using Aimrank.Common.Domain;
using System;

namespace Aimrank.Domain.Users
{
    public class UserId : EntityId, IEquatable<UserId>
    {
        public UserId(Guid value) : base(value)
        {
        }

        public bool Equals(UserId other) => this == other;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((UserId) obj);
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}