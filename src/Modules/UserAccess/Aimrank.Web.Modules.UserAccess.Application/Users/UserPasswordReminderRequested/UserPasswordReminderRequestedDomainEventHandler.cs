using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.UserAccess.Application.Services;
using Aimrank.Web.Modules.UserAccess.Domain.Users.Events;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.UserPasswordReminderRequested
{
    public class UserPasswordReminderRequestedDomainEventHandler : IDomainEventHandler<UserPasswordReminderRequestedDomainEvent>
    {
        private readonly IEmailSender _emailSender;
        private readonly IUrlFactory _urlFactory;

        public UserPasswordReminderRequestedDomainEventHandler(IEmailSender emailSender, IUrlFactory urlFactory)
        {
            _emailSender = emailSender;
            _urlFactory = urlFactory;
        }

        public async Task HandleAsync(UserPasswordReminderRequestedDomainEvent @event, CancellationToken cancellationToken = default)
        {
            var resetPasswordLink = _urlFactory.CreateResetPasswordLink(@event.User.Id, @event.Token.Token);
            
            var textBody =
                $"Hello {@event.User.Username}\n" +
                "In order to reset your password please click the following link:" +
                $"{resetPasswordLink}";

            var htmlBody = $@"
                <h1>Hello {@event.User.Username}</h1>
                <p>In order to reset your password please click the following link:</p>
                <p><a href=""{resetPasswordLink}"">Reset password</a></p>";

            var recipient = new EmailRecipient(@event.User.Username, @event.User.Email);

            await _emailSender.SendAsync(
                new[] {recipient},
                "Reset your password",
                textBody,
                htmlBody,
                cancellationToken);
        }
    }
}