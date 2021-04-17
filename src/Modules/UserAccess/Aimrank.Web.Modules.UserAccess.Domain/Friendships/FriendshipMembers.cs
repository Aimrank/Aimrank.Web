using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.UserAccess.Domain.Users;
using System.Collections.Generic;
using System;

namespace Aimrank.Web.Modules.UserAccess.Domain.Friendships
{
    public class FriendshipMembers : ValueObject, IEquatable<FriendshipMembers>
    {
        public UserId UserId1 { get; }
        public UserId UserId2 { get; }

        public FriendshipMembers(UserId userId1, UserId userId2)
        {
            UserId1 = userId1;
            UserId2 = userId2;
        }

        public bool Equals(FriendshipMembers other)
            => other is not null &&
               (other.UserId1.Equals(UserId1) && other.UserId2.Equals(UserId2)) ||
               (other.UserId1.Equals(UserId2) && other.UserId2.Equals(UserId1));

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((FriendshipMembers) obj);
        }

        public override int GetHashCode()
            => HashCode.Combine(UserId1, UserId2) ^
               HashCode.Combine(UserId2, UserId1);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId1;
            yield return UserId2;
        }
    }
}