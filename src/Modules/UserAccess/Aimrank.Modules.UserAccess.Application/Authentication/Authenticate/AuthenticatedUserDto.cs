using System.Collections.Generic;
using System.Security.Claims;
using System;

namespace Aimrank.Modules.UserAccess.Application.Authentication.Authenticate
{
    public class AuthenticatedUserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public List<Claim> Claims { get; set; }
    }
}