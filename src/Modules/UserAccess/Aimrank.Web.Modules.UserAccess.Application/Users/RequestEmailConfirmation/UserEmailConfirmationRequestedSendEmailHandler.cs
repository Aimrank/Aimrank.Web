using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.UserAccess.Application.Services;
using Aimrank.Web.Modules.UserAccess.Domain.Users.Events;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.RequestEmailConfirmation
{
    public class UserEmailConfirmationRequestedSendEmailHandler
        : INotificationHandler<DomainEventNotification<UserEmailConfirmationRequestedDomainEvent>>
    {
        private readonly IEmailSender _emailSender;
        private readonly IUrlFactory _urlFactory;

        public UserEmailConfirmationRequestedSendEmailHandler(
            IEmailSender emailSender,
            IUrlFactory urlFactory)
        {
            _emailSender = emailSender;
            _urlFactory = urlFactory;
        }

        public async Task Handle(
            DomainEventNotification<UserEmailConfirmationRequestedDomainEvent> notification,
            CancellationToken cancellationToken)
        {
            var confirmationLink = _urlFactory.CreateEmailConfirmationLink(
                notification.DomainEvent.UserId, notification.DomainEvent.Token);
            
            var textBody =
                $"Hello {notification.DomainEvent.Username}\n" +
                "In order to activate your account please click following link:\n" +
                $"{confirmationLink}";

            var htmlBody = $@"
                <h1>Hello {notification.DomainEvent.Username}</h1>
                <p>In order to activate your account please click following link:<p>
                <p><a href=""{confirmationLink}"">Confirm your E-Mail address</a></p>";

            var recipient = new EmailRecipient(notification.DomainEvent.Username, notification.DomainEvent.Email);

            await _emailSender.SendAsync(
                new[] {recipient},
                "Confirm your E-Mail address",
                textBody,
                htmlBody,
                cancellationToken);
        }
    }
}