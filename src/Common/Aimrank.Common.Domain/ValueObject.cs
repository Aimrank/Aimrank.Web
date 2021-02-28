using System.Collections.Generic;
using System.Linq;
using System;

namespace Aimrank.Common.Domain
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public bool Equals(ValueObject other)
            => other is not null && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ValueObject) obj);
        }

        public override int GetHashCode() => GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
        
        protected abstract IEnumerable<object> GetEqualityComponents();

        public static bool operator ==(ValueObject lhs, ValueObject rhs) => lhs is not null && lhs.Equals(rhs);
        public static bool operator !=(ValueObject lhs, ValueObject rhs) => !(lhs == rhs);
    }
}