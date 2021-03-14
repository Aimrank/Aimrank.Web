using Aimrank.Common.Domain;
using System;

namespace Aimrank.Modules.UserAccess.Domain.Users
{
    public class UserId : EntityId
    {
        public UserId(Guid value) : base(value)
        {
        }
    }
}