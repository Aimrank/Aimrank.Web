using Aimrank.Common.Domain;

namespace Aimrank.Modules.UserAccess.Domain.Users.Events
{
    public class UserCreatedDomainEvent : IDomainEvent
    {
        public User User { get; }
        public UserToken EmailConfirmationToken { get; }

        public UserCreatedDomainEvent(User user, UserToken emailConfirmationToken)
        {
            User = user;
            EmailConfirmationToken = emailConfirmationToken;
        }
    }
}