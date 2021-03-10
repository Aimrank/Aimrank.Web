using System;

namespace Aimrank.Application.Queries.Users.GetUserBatch
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string SteamId { get; set; }
        public string Username { get; set; }
    }
}