using Aimrank.Web.Common.Domain;
using Aimrank.Web.Modules.UserAccess.Application.Services;
using Aimrank.Web.Modules.UserAccess.Domain.Users.Events;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Web.Modules.UserAccess.Application.Users.UserCreated
{
    public class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        private readonly IEmailSender _emailSender;
        private readonly IUrlFactory _urlFactory;

        public UserCreatedDomainEventHandler(IEmailSender emailSender, IUrlFactory urlFactory)
        {
            _emailSender = emailSender;
            _urlFactory = urlFactory;
        }

        public async Task HandleAsync(UserCreatedDomainEvent @event, CancellationToken cancellationToken = default)
        {
            var confirmationLink = _urlFactory.CreateEmailConfirmationLink(@event.User.Id, @event.Token.Token);

            var textBody =
                $"Hello {@event.User.Username}\n" +
                "Welcome on Aimrank.Web. In order to activate your account please click following link:\n" +
                $"{confirmationLink}";

            var htmlBody = $@"
                <h1>Hello {@event.User.Username}</h1>
                <p>Welcome on Aimrank.Web. In order to activate your account please click following link:<p>
                <p><a href=""{confirmationLink}"">Confirm your E-Mail address</a></p>";

            var recipient = new EmailRecipient(@event.User.Username, @event.User.Email);

            await _emailSender.SendAsync(
                new[] {recipient}, 
                "Welcome on Aimrank",
                textBody,
                htmlBody,
                cancellationToken);
        }
    }
}