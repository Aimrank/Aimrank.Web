using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.UserAccess.Domain.Users.Events
{
    public class UserCreatedDomainEvent : DomainEvent
    {
        public UserId UserId { get; }
        public string Username { get; }
        public string Email { get; }
        public string Token { get; }

        public UserCreatedDomainEvent(UserId userId, string username, string email, string token)
        {
            UserId = userId;
            Username = username;
            Email = email;
            Token = token;
        }
    }
}