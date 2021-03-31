using Aimrank.Common.Domain;

namespace Aimrank.Modules.UserAccess.Domain.Users.Events
{
    public class UserEmailConfirmationRequestedDomainEvent : IDomainEvent
    {
        public User User { get; }
        public UserToken Token { get; }

        public UserEmailConfirmationRequestedDomainEvent(User user, UserToken token)
        {
            User = user;
            Token = token;
        }
    }
}