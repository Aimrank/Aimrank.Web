using Aimrank.Web.Common.Domain;

namespace Aimrank.Web.Modules.UserAccess.Domain.Users.Events
{
    public class UserPasswordReminderRequestedDomainEvent : IDomainEvent
    {
        public User User { get; }
        public UserToken Token { get; }

        public UserPasswordReminderRequestedDomainEvent(User user, UserToken token)
        {
            User = user;
            Token = token;
        }
    }
}