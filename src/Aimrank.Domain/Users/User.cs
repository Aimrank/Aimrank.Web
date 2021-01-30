using Aimrank.Common.Domain;
using Aimrank.Domain.Users.Rules;
using Microsoft.AspNetCore.Identity;

namespace Aimrank.Domain.Users
{
    public class User : IdentityUser
    {
        public string SteamId { get; private set; }

        private User() {}

        public User(string id, string email, string username)
        {
            Id = id;
            Email = email;
            UserName = username;
        }

        public void SetSteamId(string steamId)
        {
            BusinessRules.Check(new SteamIdMustBeValidRule(steamId));

            SteamId = steamId;
        }
    }
}