using Aimrank.Common.Domain;
using Aimrank.Modules.UserAccess.Domain.Users.Events;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Aimrank.Modules.UserAccess.Application.Users.UserCreated
{
    public class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        public Task HandleAsync(UserCreatedDomainEvent @event, CancellationToken cancellationToken = default)
        {
            Console.WriteLine($"Send email to {@event.User.Email} with id {@event.User.Id.Value} with token {@event.EmailConfirmationToken.Token}");
            
            // Todo
            // Inject email service and send email
            // Generate url that user has to click on
            
            return Task.CompletedTask;
        }
    }
}