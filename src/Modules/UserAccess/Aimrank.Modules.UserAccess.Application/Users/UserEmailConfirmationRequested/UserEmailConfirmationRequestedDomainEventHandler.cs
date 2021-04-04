using Aimrank.Common.Domain;
using Aimrank.Modules.UserAccess.Application.Services;
using Aimrank.Modules.UserAccess.Domain.Users.Events;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.UserAccess.Application.Users.UserEmailConfirmationRequested
{
    public class UserEmailConfirmationRequestedDomainEventHandler : IDomainEventHandler<UserEmailConfirmationRequestedDomainEvent>
    {
        private readonly IEmailSender _emailSender;
        private readonly IUrlFactory _urlFactory;

        public UserEmailConfirmationRequestedDomainEventHandler(IEmailSender emailSender, IUrlFactory urlFactory)
        {
            _emailSender = emailSender;
            _urlFactory = urlFactory;
        }

        public async Task HandleAsync(UserEmailConfirmationRequestedDomainEvent @event, CancellationToken cancellationToken = default)
        {
            var confirmationLink = _urlFactory.CreateEmailConfirmationLink(@event.User.Id, @event.Token.Token);
            
            var textBody =
                $"Hello {@event.User.Username}\n" +
                "In order to activate your account please click following link:\n" +
                $"{confirmationLink}";

            var htmlBody = $@"
                <h1>Hello {@event.User.Username}</h1>
                <p>In order to activate your account please click following link:<p>
                <p><a href=""{confirmationLink}"">Confirm your E-Mail address</a></p>";

            var recipient = new EmailRecipient(@event.User.Username, @event.User.Email);

            await _emailSender.SendAsync(
                new[] {recipient},
                "Confirm your E-Mail address",
                textBody,
                htmlBody,
                cancellationToken);
        }
    }
}