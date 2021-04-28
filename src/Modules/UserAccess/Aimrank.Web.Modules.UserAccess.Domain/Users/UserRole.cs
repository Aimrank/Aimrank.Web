using Aimrank.Web.Common.Domain;
using System.Collections.Generic;

namespace Aimrank.Web.Modules.UserAccess.Domain.Users
{
    public class UserRole : ValueObject
    {
        public static UserRole Admin => new UserRole(nameof(Admin));
        
        public string Name { get; }

        private UserRole(string name)
        {
            Name = name;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}