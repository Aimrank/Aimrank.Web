using Aimrank.Modules.UserAccess.Application.Contracts;
using Aimrank.Modules.UserAccess.Domain.Users;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Modules.UserAccess.Application.Users.UpdateSteamDetails
{
    internal class UpdateSteamDetailsCommandHandler : ICommandHandler<UpdateSteamDetailsCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateSteamDetailsCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateSteamDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(new UserId(request.UserId));

            await user.SetSteamIdAsync(request.SteamId, _userRepository);
            
            _userRepository.Update(user);
            
            return Unit.Value;
        }
    }
}