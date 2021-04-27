using System.Collections.Generic;
using System.Security.Claims;
using System;

namespace Aimrank.Web.Modules.UserAccess.Application.Authentication.Authenticate
{
    internal class UserResultDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public IList<string> Roles { get; set; }

        public AuthenticatedUserDto AsAuthenticatedUserDto()
        {
            var result = new AuthenticatedUserDto
            {
                Id = Id,
                Email = Email,
                Username = Username,
                Claims = new List<Claim>
                {
                    new(ClaimTypes.Email, Email),
                    new(ClaimTypes.Name, Username),
                    new(ClaimTypes.NameIdentifier, Id.ToString())
                }
            };
            
            foreach (var role in Roles)
            {
                result.Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return result;
        }
    }
}