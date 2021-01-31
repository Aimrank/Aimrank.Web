using System;

namespace Aimrank.Application.Queries.GetUserDetails
{
    public class UserDetailsDto
    {
        public Guid UserId { get; init; }
        public string SteamId { get; init; }
        public string Username { get; init; }
    }
}