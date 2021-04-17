using System;

namespace Aimrank.Web.Common.Domain
{
    public abstract class EntityId : IEquatable<EntityId>
    {
        public Guid Value { get; }

        protected EntityId(Guid value)
        {
            BusinessRules.Check(new EntityIdMustBeValidRule(value));
            
            Value = value;
        }
        
        public bool Equals(EntityId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((EntityId) obj);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public static implicit operator Guid(EntityId id) => id.Value;

        public static bool operator ==(EntityId lhs, EntityId rhs) => lhs is not null && lhs.Equals(rhs);
        public static bool operator !=(EntityId lhs, EntityId rhs) => !(lhs == rhs);
    }
}