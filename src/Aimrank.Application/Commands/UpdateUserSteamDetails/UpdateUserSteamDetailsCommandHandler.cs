using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.UpdateUserSteamDetails
{
    public class UpdateUserSteamDetailsCommandHandler : ICommandHandler<UpdateUserSteamDetailsCommand>
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

            user.SetSteamId(request.SteamId);
            
            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}