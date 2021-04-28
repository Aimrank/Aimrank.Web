using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.UserAccess.Domain.Users.Events;
using Aimrank.Web.Modules.UserAccess.Domain.Users.Rules;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Aimrank.Web.Modules.UserAccess.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        public UserId Id { get; }
        public string Email { get; }
        public string Username { get; }
        public bool IsActive { get; private set; }
        
        private string _password;
        private readonly List<UserRole> _roles = new();
        private readonly List<UserToken> _tokens = new();

        private User() {}

        private User(UserId id, string email, string username, string password, UserToken emailConfirmationToken)
        {
            Id = id;
            Email = email;
            Username = username;
            IsActive = false;
            _password = password;
            _tokens = new List<UserToken> {emailConfirmationToken};
        }

        public static async Task<User> CreateAsync(
            UserId id,
            string email,
            string username,
            string password,
            IUserRepository userRepository)
        {
            await BusinessRules.CheckAsync(new EmailMustBeUniqueRule(userRepository, email));
            await BusinessRules.CheckAsync(new UsernameMustBeUniqueRule(userRepository, username));

            var token = UserToken.Create(UserTokenType.EmailConfirmation);

            var user = new User(id, email, username, password, token);
            
            user.AddDomainEvent(new UserCreatedDomainEvent(user.Id, username, email, token.Token));

            return user;
        }

        public void ConfirmEmail(string token)
        {
            BusinessRules.Check(new UserTokenMustBeValidRule(UserTokenType.EmailConfirmation, _tokens, token));

            _tokens.RemoveAll(t => t.Type == UserTokenType.EmailConfirmation);
            
            IsActive = true;
        }

        public void RequestEmailConfirmation()
        {
            BusinessRules.Check(new UserMustNotBeActiveRule(this));

            _tokens.RemoveAll(t => t.Type == UserTokenType.EmailConfirmation);

            var token = UserToken.Create(UserTokenType.EmailConfirmation);
            
            _tokens.Add(token);
            
            AddDomainEvent(new UserEmailConfirmationRequestedDomainEvent(Id, Username, Email, token.Token));
        }

        public void RequestPasswordReminder()
        {
            BusinessRules.Check(new UserMustBeActiveRule(this));
            
            _tokens.RemoveAll(t => t.Type == UserTokenType.PasswordReminder);

            var token = UserToken.Create(UserTokenType.PasswordReminder, DateTime.UtcNow.AddMinutes(10));
            
            _tokens.Add(token);
            
            AddDomainEvent(new UserPasswordReminderRequestedDomainEvent(Id, Username, Email, token.Token));
        }

        public void ChangePassword(string oldPassword, string newPasswordHash)
        {
            BusinessRules.Check(new PasswordMustMatchRule(oldPassword, _password));

            _password = newPasswordHash;
        }

        public void ResetPassword(string newPasswordHash, string token)
        {
            BusinessRules.Check(new UserTokenMustBeValidRule(UserTokenType.PasswordReminder, _tokens, token));

            _tokens.RemoveAll(t => t.Type == UserTokenType.PasswordReminder);

            _password = newPasswordHash;
        }
    }
}