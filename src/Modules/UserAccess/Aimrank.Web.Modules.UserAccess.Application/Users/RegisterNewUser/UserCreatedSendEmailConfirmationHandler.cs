using Aimrank.Web.Common.Application.Events;
using Aimrank.Web.Modules.UserAccess.Application.Services;
using Aimrank.Web.Modules.UserAccess.Domain.Users.Events;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.RegisterNewUser
{
    public class UserCreatedSendEmailConfirmationHandler
        : INotificationHandler<DomainEventNotification<UserCreatedDomainEvent>>
    {
        private readonly IEmailSender _emailSender;
        private readonly IUrlFactory _urlFactory;

        public UserCreatedSendEmailConfirmationHandler(IEmailSender emailSender, IUrlFactory urlFactory)
        {
            _emailSender = emailSender;
            _urlFactory = urlFactory;
        }

        public async Task Handle(DomainEventNotification<UserCreatedDomainEvent> notification, CancellationToken cancellationToken)
        {
            var confirmationLink = _urlFactory.CreateEmailConfirmationLink(
                notification.DomainEvent.UserId, notification.DomainEvent.Token);

            var textBody =
                $"Hello {notification.DomainEvent.Username}\n" +
                "Welcome on Aimrank.Web. In order to activate your account please click following link:\n" +
                $"{confirmationLink}";

            var htmlBody = $@"
                <h1>Hello {notification.DomainEvent.Username}</h1>
                <p>Welcome on Aimrank.Web. In order to activate your account please click following link:<p>
                <p><a href=""{confirmationLink}"">Confirm your E-Mail address</a></p>";

            var recipient = new EmailRecipient(notification.DomainEvent.Username, notification.DomainEvent.Email);

            await _emailSender.SendAsync(
                new[] {recipient}, 
                "Welcome on Aimrank",
                textBody,
                htmlBody,
                cancellationToken);
        }
    }
}