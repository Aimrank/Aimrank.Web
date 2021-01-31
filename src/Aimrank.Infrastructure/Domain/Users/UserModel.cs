using Aimrank.Domain.Users;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace Aimrank.Infrastructure.Domain.Users
{
    internal class UserModel : IdentityUser<UserId>
    {
        public string SteamId { get; set; }

        public User AsUser()
        {
            var type = typeof(User);
            var types = new[] {typeof(UserId), typeof(string), typeof(string)};
            var constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, types, null);
            var user = (User) constructor?.Invoke(new object[]{Id, Email, UserName});
            type.GetProperty(nameof(user.SteamId))?.SetValue(user, SteamId);
            return user;
        }
    }
}