using Aimrank.Web.Common.Domain;
using System;

namespace Aimrank.Web.Modules.UserAccess.Domain.Users
{
    public class UserId : EntityId
    {
        public UserId(Guid value) : base(value)
        {
        }
    }
}