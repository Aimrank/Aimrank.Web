using Aimrank.Application.Contracts;
using Aimrank.Common.Application.Exceptions;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.Users.UpdateUserSteamDetails
{
    internal class UpdateUserSteamDetailsCommandHandler : ICommandHandler<UpdateUserSteamDetailsCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserSteamDetailsCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUserSteamDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(new UserId(request.UserId));
            if (user is null)
            {
                throw new EntityNotFoundException();
            }

            await user.SetSteamIdAsync(request.SteamId, _userRepository);
            
            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}