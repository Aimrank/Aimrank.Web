using System;

namespace Aimrank.Application.Queries.GetUserDetails
{
    public class UserDetailsDto
    {
        public Guid UserId { get; set; }
        public string SteamId { get; set; }
        public string Username { get; set; }
    }
}