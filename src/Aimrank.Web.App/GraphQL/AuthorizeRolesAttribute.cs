using HotChocolate.AspNetCore.Authorization;
using System;

namespace Aimrank.Web.App.GraphQL
{
    [AttributeUsage(
        AttributeTargets.Class
        | AttributeTargets.Struct
        | AttributeTargets.Property
        | AttributeTargets.Method,
        AllowMultiple = true)]
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles)
        {
            Roles = roles;
        }
    }
}