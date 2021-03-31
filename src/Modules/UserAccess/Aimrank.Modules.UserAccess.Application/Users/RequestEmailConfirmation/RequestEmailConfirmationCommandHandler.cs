using Aimrank.Common.Application.Exceptions;
using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.UserAccess.Application.Users.RequestEmailConfirmation
{
    internal class RequestEmailConfirmationCommandHandler : ICommandHandler<RequestEmailConfirmationCommand>
    {
        private readonly IUserRepository _userRepository;

        public RequestEmailConfirmationCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(RequestEmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailOptionalAsync(request.UsernameOrEmail) ??
                       await _userRepository.GetByUsernameOptionalAsync(request.UsernameOrEmail);

            if (user is null)
            {
                throw new EntityNotFoundException();
            }
            
            user.RequestEmailConfirmation();
            
            return Unit.Value;
        }
    }
}