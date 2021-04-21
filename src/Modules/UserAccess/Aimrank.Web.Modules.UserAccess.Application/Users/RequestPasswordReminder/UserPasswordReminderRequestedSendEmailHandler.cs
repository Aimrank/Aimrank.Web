using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.UserAccess.Application.Services;
using Aimrank.Web.Modules.UserAccess.Domain.Users.Events;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.RequestPasswordReminder
{
    public class UserPasswordReminderRequestedSendEmailHandler
        : INotificationHandler<DomainEventNotification<UserPasswordReminderRequestedDomainEvent>>
    {
        private readonly IEmailSender _emailSender;
        private readonly IUrlFactory _urlFactory;

        public UserPasswordReminderRequestedSendEmailHandler(IEmailSender emailSender, IUrlFactory urlFactory)
        {
            _emailSender = emailSender;
            _urlFactory = urlFactory;
        }

        public async Task Handle(
            DomainEventNotification<UserPasswordReminderRequestedDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            var resetPasswordLink = _urlFactory.CreateResetPasswordLink(
                notification.DomainEvent.UserId, notification.DomainEvent.Token);
            
            var textBody =
                $"Hello {notification.DomainEvent.Username}\n" +
                "In order to reset your password please click the following link:" +
                $"{resetPasswordLink}";

            var htmlBody = $@"
                <h1>Hello {notification.DomainEvent.Username}</h1>
                <p>In order to reset your password please click the following link:</p>
                <p><a href=""{resetPasswordLink}"">Reset password</a></p>";

            var recipient = new EmailRecipient(notification.DomainEvent.Username, notification.DomainEvent.Email);

            await _emailSender.SendAsync(
                new[] {recipient},
                "Reset your password",
                textBody,
                htmlBody,
                cancellationToken);
        }
    }
}