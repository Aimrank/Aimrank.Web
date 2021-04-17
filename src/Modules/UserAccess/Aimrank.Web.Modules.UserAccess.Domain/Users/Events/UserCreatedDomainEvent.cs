using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.UserAccess.Domain.Users.Events
{
    public class UserCreatedDomainEvent : IDomainEvent
    {
        public User User { get; }
        public UserToken Token { get; }

        public UserCreatedDomainEvent(User user, UserToken token)
        {
            User = user;
            Token = token;
        }
    }
}