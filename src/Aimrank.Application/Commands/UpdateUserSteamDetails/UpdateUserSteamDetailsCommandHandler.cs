using Aimrank.Application.Contracts;
using Aimrank.Common.Application;
using Aimrank.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Threading;

namespace Aimrank.Application.Commands.UpdateUserSteamDetails
{
    public class UpdateUserSteamDetailsCommandHandler : ICommandHandler<UpdateUserSteamDetailsCommand>
    {
        private readonly UserManager<User> _userManager;

        public UpdateUserSteamDetailsCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(UpdateUserSteamDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
            {
                throw new EntityNotFoundException();
            }

            user.SetSteamId(request.SteamId);

            return Unit.Value;
        }
    }
}